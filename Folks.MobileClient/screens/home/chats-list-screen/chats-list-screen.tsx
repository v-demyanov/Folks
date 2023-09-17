import { View } from 'react-native';

import HomeSearchbar from '../../../components/home/searchbar/home-searchbar';
import ChatsList from '../../../features/chats/chats-list/chats-list';
import ChatCreateButton from '../../../features/chats/chat-create-button/chat-create-button';

const ChatsListScreen = (): JSX.Element => {
  return (
    <>
      <View>
        <HomeSearchbar />
        <ChatsList />
      </View>
      <ChatCreateButton />
    </>
  );
};

export default ChatsListScreen;
