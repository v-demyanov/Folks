import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { FormikProps, useFormik } from 'formik';

import GroupHeader from '../../../features/groups/components/group-header/group-header';
import MessageInput from '../../../features/messages/components/message-input/message-input';
import MessagesList from '../../../features/messages/components/messages-list/messages-list';
import { RootStackParamList } from '../../../navigation/app-navigator';
import { useSendMessageMutation } from '../../../features/messages/api/messages.api';
import ISendMessageFormValue from '../../../features/messages/models/send-message-form-value';
import SendMessageFormValidationSchema from '../../../features/messages/validation/send-message-form.validation';
import ICreateMessageCommand from '../../../features/messages/models/create-message-command';
import useAuth from '../../../features/auth/hooks/use-auth';
import ChannelType from '../../../features/channels/enums/channel-type';
import * as MessagesConstants from '../../../features/messages/constants/messages.constants';

type Props = NativeStackScreenProps<RootStackParamList, 'Group'>;

const GroupScreen = ({ route }: Props): JSX.Element => {
  const { currentUser } = useAuth();
  const [sendMessage, { isLoading: isSendingMessage }] =
    useSendMessageMutation();
  const group = route.params;

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
            channelId: group.id,
            channelType: ChannelType.Group,
            content: contentFragment,
            sentAt: new Date(),
          } as ICreateMessageCommand;

          await sendMessage(createMessageCommand).unwrap();
          sendMessageForm.resetForm();
        }
      },
    });

  function splitContentOnFragments(content: string): string[] {
    const matcher = new RegExp(
      `.{1,${MessagesConstants.CONTENT_MAXIMUM_LENGTH}}`,
      'g'
    );

    return (
      content
        .match(matcher)
        ?.map((x) => x.trim())
        .filter((x) => x.length > 0) || []
    );
  }

  return (
    <>
      <GroupHeader group={group} />
      <MessagesList />
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
