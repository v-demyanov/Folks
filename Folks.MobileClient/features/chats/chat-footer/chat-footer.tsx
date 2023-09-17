import { View } from 'react-native';
import { StyleSheet } from 'react-native';

import ChatInput from './chat-input/chat-input';
import MessageSendButton from './message-send-button/message-send-button';

const styles = StyleSheet.create({
  view: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingLeft: 10,
    paddingRight: 5,
  },
});

const ChatFooter = (): JSX.Element => {
  return (
    <View style={[styles.view]}>
      <ChatInput />
      <MessageSendButton />
    </View>
  );
};

export default ChatFooter;
