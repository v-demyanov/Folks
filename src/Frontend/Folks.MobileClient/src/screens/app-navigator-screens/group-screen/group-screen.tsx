import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { FormikProps, useFormik } from 'formik';
import { useState } from 'react';

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

type Props = NativeStackScreenProps<RootStackParamList, 'Group'>;

const GroupScreen = ({ route }: Props): JSX.Element => {
  const channel = route.params;
  const { currentUser } = useAuth();

  const [sendMessage, { isLoading: isSendingMessage }] =
    useSendMessageMutation();
  const { data: messages = [] } = useGetMessagesQuery({
    channelId: channel.id,
    channelType: channel.type,
  } as IGetMessagesQuery);

  const [messagesSections, setMessagesSections] = useState<
    ISectionListItem<Date, IMessagesListItem>[]
  >([]);

  useArrayEffect(() => {
    const messagesSections = prepareMessagesSections();
    setMessagesSections(messagesSections);
  }, [messages, currentUser]);

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

  return (
    <>
      <GroupHeader group={channel} />
      <MessagesList sections={messagesSections} />
      <MessageInput
        onSendPress={sendMessageForm.handleSubmit}
        form={sendMessageForm}
        sendDisabled={
          !sendMessageForm.isValid || !sendMessageForm.dirty || isSendingMessage
        }
      />
    </>
  );
};

export default GroupScreen;
