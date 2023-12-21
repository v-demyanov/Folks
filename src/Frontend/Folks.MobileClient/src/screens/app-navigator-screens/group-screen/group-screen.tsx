import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { FormikProps, useFormik } from 'formik';
import { useEffect, useState } from 'react';
import { Snackbar, useTheme } from 'react-native-paper';

import GroupHeader from '../../../features/groups/components/group-header/group-header';
import MessageInput from '../../../features/messages/components/message-input/message-input';
import MessagesList from '../../../features/messages/components/messages-list/messages-list';
import { RootStackParamList } from '../../../navigation/app-navigator';
import {
  useGetMessagesQuery,
  useSendMessageMutation,
} from '../../../features/messages/api/messages.api';
import ISendMessageFormValue from '../../../features/messages/models/send-message-form-value';
import SendMessageFormValidationSchema from '../../../features/messages/validation/send-message-form.validation';
import ICreateMessageCommand from '../../../features/messages/models/create-message-command';
import useAuth from '../../../features/auth/hooks/use-auth';
import IGetMessagesQuery from '../../../features/messages/models/get-messages-query';
import IMessagesListItem from '../../../features/messages/models/messages-list-item';
import ISectionListItem from '../../../common/models/section-list-item';
import { mapMessagesListItem } from '../../../features/messages/helpers/messages-list-item.helpers';
import {
  groupMessagesByDate,
  splitContentOnFragments,
} from '../../../features/messages/helpers/message.helpers';
import useArrayEffect from '../../../common/hooks/use-array-effect';
import InformationContainer from '../../../common/components/information-container/information-container';
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
    } as IGetMessagesQuery);

  const [messagesSections, setMessagesSections] = useState<
    ISectionListItem<Date, IMessagesListItem>[]
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
    IMessagesListItem
  >[] {
    if (!currentUser) {
      return [];
    }

    const messagesGroups = groupMessagesByDate(messages);
    const sections: ISectionListItem<Date, IMessagesListItem>[] = [];

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
          } as ICreateMessageCommand;

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
