import { IconButton } from 'react-native-paper';

import { SEND_MESSAGE_BUTTON_ICON_SIZE } from '../../../../common/constants/icons.constants';

const MessageSendButton = (): JSX.Element => {
  return (
    <IconButton
      icon="send"
      size={SEND_MESSAGE_BUTTON_ICON_SIZE}
      onPress={() => {}}
    />
  );
};

export default MessageSendButton;
