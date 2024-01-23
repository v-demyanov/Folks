import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { FormikProps, useFormik } from 'formik';
import { useEffect, useState } from 'react';
import { Snackbar, useTheme } from 'react-native-paper';

import { RootStackParamList } from '../../../navigation/app-navigator';
import {
  useGetMessagesQuery,
  useSendMessageMutation,
  SendMessageFormValidationSchema,
} from '../../../features/messages';
import {
  MessageInput,
  MessagesList,
} from '../../../features/messages/components';
import {
  ISendMessageFormValue,
  ICreateMessageRequest,
  IGetMessagesRequest,
  MessagesListItem,
} from '../../../features/messages/models';
import {
  groupMessagesByDate,
  splitContentOnFragments,
  mapMessagesListItem,
} from '../../../features/messages/helpers';
import { useAuth } from '../../../features/auth/hooks';
import { ISectionListItem } from '../../../common/models';
import { useArrayEffect } from '../../../common/hooks';
import { InformationContainer } from '../../../common/components';
import { Theme } from '../../../themes/types/theme';
import { GroupHeader } from '../../../features/groups/components';

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

  const [messagesSections, setMessagesSections] = useState<
    ISectionListItem<Date, MessagesListItem>[]
  >([]);
  const [sendMessageErrorVisible, setSendMessageErrorVisible] = useState(false);

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

  return (
    <>
      <GroupHeader group={channel} />
      {isGetMessagesError ? (
        <InformationContainer
          message={getMessagesErrorMessage()}
          backgroudColor={theme.colors.secondaryContainer}
        />
      ) : (
        <MessagesList sections={messagesSections} />
      )}
      <MessageInput
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
