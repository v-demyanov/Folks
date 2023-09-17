import { List } from 'react-native-paper';
import { ScrollView } from 'react-native';

import IChat from '../../models/chat';
import ChatsListItem from './chats-list-item/chats-list-item';

const chats: IChat[] = Array.from(Array(10).keys()).map((id) => ({
  id: id,
  title: `Chat ${id}`,
  avatartImagePath: `../path`,
  members: [],
  createdDate: new Date(),
  lastUpdateDate: new Date(),
}));

const ChatsList = (): JSX.Element => {
  const renderedChats = chats.map((chat) => (
    <ChatsListItem
      key={chat.id}
      title={chat.title ?? ''}
      imagePath={chat.avatartImagePath ?? ''}
      lastUpdateDate={chat.lastUpdateDate}
    />
  ));

  return (
    <ScrollView>
      <List.Section>{renderedChats}</List.Section>
    </ScrollView>
  );
};

export default ChatsList;
