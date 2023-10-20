import { IconButton } from 'react-native-paper';
import { StyleSheet } from 'react-native';

import { CHAT_CREATE_BUTTON_ICON_SIZE } from '../../../common/constants/icons.constants';

const styles = StyleSheet.create({
  button: {
    position: 'absolute',
    right: 0,
    bottom: 0,
    marginBottom: 20,
    marginRight: 20,
  },
});

const ChatCreateButton = (): JSX.Element => {
  return (
    <IconButton
      style={[styles.button]}
      icon="chat-plus-outline"
      size={CHAT_CREATE_BUTTON_ICON_SIZE}
      mode="contained"
    />
  );
};

export default ChatCreateButton;
