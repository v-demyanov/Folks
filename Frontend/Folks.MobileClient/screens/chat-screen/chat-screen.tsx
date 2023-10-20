import { View } from 'react-native';
import { StyleSheet } from 'react-native';

import ChatFooter from '../../features/chats/chat-footer/chat-footer';
import ChatHeader from '../../features/chats/chat-header/chat-header';
import ChatMessageList from '../../features/chats/chat-message-list/chat-message-list';

const styles = StyleSheet.create({
  view: {
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'space-between',
    height: '100%',
  },
});

const ChatScreen = (): JSX.Element => {
  return (
    <View style={[styles.view]}>
      <ChatHeader />
      <ChatMessageList />
      <ChatFooter />
    </View>
  );
};

export default ChatScreen;
