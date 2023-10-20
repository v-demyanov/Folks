import { IconButton } from 'react-native-paper';

import { CHAT_SEND_MESSAGE_BUTTON_ICON_SIZE } from '../../../../common/constants/icons.constants';

const MessageSendButton = (): JSX.Element => {
  return (
    <IconButton
      icon="send"
      size={CHAT_SEND_MESSAGE_BUTTON_ICON_SIZE}
      onPress={() => {}}
    />
  );
};

export default MessageSendButton;
