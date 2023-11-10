import { FlatList } from 'react-native';
import { useCallback } from 'react';

import IChat from '../../models/chat';
import ChatsListItem from './chats-list-item/chats-list-item';

const chats: IChat[] = Array.from(Array(10).keys())
  .map((id) => ({
    id: id,
    title: `Chat ${id}`,
    avatartImagePath: `../path`,
    members: [],
    createdDate: new Date(),
    lastUpdateDate: new Date(),
  }))
  .reverse();

const ChatsList = (): JSX.Element => {
  const renderChatItem = useCallback(
    ({ item }: { item: IChat }) => (
      <ChatsListItem
        key={item.id}
        title={item.title ?? ''}
        imagePath={item.avatartImagePath ?? ''}
        lastUpdateDate={item.lastUpdateDate}
      />
    ),
    []
  );

  return (
    <FlatList
      data={chats}
      renderItem={renderChatItem}
      keyExtractor={(item) => item.id.toString()}
    />
  );
};

export default ChatsList;
