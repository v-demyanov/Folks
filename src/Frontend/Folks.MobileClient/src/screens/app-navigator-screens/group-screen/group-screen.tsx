import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { FormikProps, useFormik } from 'formik';
import { useCallback, useEffect, useState } from 'react';
import { Snackbar, useTheme } from 'react-native-paper';

import { InformationContainer } from '../../../common/components';
import { useArrayEffect, useSet } from '../../../common/hooks';
import {
  ISectionListItem,
  IViewableItemsChangedEventInfo,
} from '../../../common/models';
import { useAuth } from '../../../features/auth/hooks';
import { GroupHeader } from '../../../features/groups/components';
import {
  useGetMessagesQuery,
  useSendMessageMutation,
  SendMessageFormValidationSchema,
  useReadMessageContentsMutation,
} from '../../../features/messages';
import {
  MessageInputComponent,
  MessagesListComponent,
} from '../../../features/messages/components';
import {
  groupMessagesByDate,
  splitContentOnFragments,
  mapMessagesListItem,
} from '../../../features/messages/helpers';
import {
  ISendMessageFormValue,
  ICreateMessageRequest,
  IGetMessagesRequest,
  MessagesListItem,
} from '../../../features/messages/models';
import { RootStackParamList } from '../../../navigation/app-navigator';
import { Theme } from '../../../themes/types/theme';

type Props = NativeStackScreenProps<RootStackParamList, 'Group'>;

const GroupScreen = ({ route }: Props): JSX.Element => {
  const channel = route.params;
  const { currentUser } = useAuth();
  const theme = useTheme<Theme>();

  const [
    sendMessage,
    { isLoading: isSendingMessage, isError: isSendMessageError },
  ] = useSendMessageMutation();
  const { data: messages = [], isError: isGetMessagesError } =
    useGetMessagesQuery({
      channelId: channel.id,
      channelType: channel.type,
    } as IGetMessagesRequest);
  const [readMessageContents] = useReadMessageContentsMutation();

  const [messagesSections, setMessagesSections] = useState<
    ISectionListItem<Date, MessagesListItem>[]
  >([]);
  const [sendMessageErrorVisible, setSendMessageErrorVisible] = useState(false);
  const alreadySeenMessages = useSet<string>([]);

  useArrayEffect(() => {
    const messagesSections = prepareMessagesSections();
    setMessagesSections(messagesSections);
  }, [messages, currentUser]);

  useEffect(() => {
    setSendMessageErrorVisible(isSendMessageError);
  }, [isSendMessageError]);

  function prepareMessagesSections(): ISectionListItem<
    Date,
    MessagesListItem
  >[] {
    if (!currentUser) {
      return [];
    }

    const messagesGroups = groupMessagesByDate(messages);
    const sections: ISectionListItem<Date, MessagesListItem>[] = [];

    for (const messagesGroup of messagesGroups) {
      const messagesListItems = messagesGroup[1]
        .map((message) => mapMessagesListItem(message, currentUser.sub))
        .sort((x, y) => y.sentAt.getTime() - x.sentAt.getTime());

      sections.push({
        footer: new Date(messagesGroup[0]),
        data: messagesListItems,
      });
    }

    return sections.sort((x, y) => y.footer.getTime() - x.footer.getTime());
  }

  const sendMessageForm: FormikProps<ISendMessageFormValue> =
    useFormik<ISendMessageFormValue>({
      initialValues: {},
      enableReinitialize: true,
      validationSchema: SendMessageFormValidationSchema,
      onSubmit: async (value: ISendMessageFormValue) => {
        if (!currentUser || !value.content) {
          return;
        }

        const contentFragments = splitContentOnFragments(value.content);
        for (const contentFragment of contentFragments) {
          const createMessageCommand = {
            ownerId: currentUser.sub,
            channelId: channel.id,
            channelType: channel.type,
            content: contentFragment,
            sentAt: new Date(),
          } as ICreateMessageRequest;

          await sendMessage(createMessageCommand).unwrap();
          sendMessageForm.resetForm();
        }
      },
    });

  function getMessagesErrorMessage(): string {
    return 'Oops! Something went wrong,\n while messages loading.';
  }

  function getSendMessageErrorMessage(): string {
    return 'Oops! Something went wrong, while message sending.';
  }

  function isSendMessageDisabled(): boolean {
    return (
      !sendMessageForm.isValid ||
      !sendMessageForm.dirty ||
      isSendingMessage ||
      isGetMessagesError
    );
  }

  const handleMessagesListViewableItemsChanged = useCallback(
    async (info: IViewableItemsChangedEventInfo): Promise<void> => {
      const visibleItems = info.changed
        .filter((x) => x.isViewable)
        .map((x) => x.item);
      const messageIdsToRead: string[] = [];

      for (const visibleItem of visibleItems) {
        const isMessagesListItem = visibleItem.readBy !== undefined;
        if (!isMessagesListItem) {
          continue;
        }

        const messagesListItem = visibleItem as MessagesListItem;
        const isMessageReadByCurrentUser =
          messagesListItem.readBy.some((x) => x.id === currentUser?.sub) ||
          alreadySeenMessages.has(messagesListItem.id);

        if (isMessageReadByCurrentUser) {
          continue;
        }

        messageIdsToRead.push(messagesListItem.id);
        alreadySeenMessages.add(messagesListItem.id);
      }

      if (!messageIdsToRead.length) {
        return;
      }

      try {
        await readMessageContents({
          messageIds: messageIdsToRead,
          channelId: channel.id,
          channelType: channel.type,
        }).unwrap();
      } catch {
        messageIdsToRead.forEach((id) => alreadySeenMessages.delete(id));
      }
    },
    [],
  );

  return (
    <>
      <GroupHeader group={channel} />
      {isGetMessagesError ? (
        <InformationContainer
          message={getMessagesErrorMessage()}
          backgroudColor={theme.colors.secondaryContainer}
        />
      ) : (
        <MessagesListComponent
          sections={messagesSections}
          onViewableItemsChanged={handleMessagesListViewableItemsChanged}
        />
      )}
      <MessageInputComponent
        onSendPress={sendMessageForm.handleSubmit}
        form={sendMessageForm}
        sendDisabled={isSendMessageDisabled()}
      />
      <Snackbar
        visible={sendMessageErrorVisible}
        onDismiss={() => setSendMessageErrorVisible(false)}
        action={{
          label: 'Close',
          onPress: () => setSendMessageErrorVisible(false),
        }}
      >
        {getSendMessageErrorMessage()}
      </Snackbar>
    </>
  );
};

export default GroupScreen;
