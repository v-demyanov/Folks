import { FlatList } from 'react-native';
import { Text } from 'react-native-paper';

import ChannelsListItem from './channels-list-item/channels-list-item';
import IChannel from '../../models/channel';
import { useGetOwnChannelsQuery } from '../../api/channels.api';

const ChannelsList = (): JSX.Element => {
  const { data: channels, error, isLoading } = useGetOwnChannelsQuery(null);

  if (error) {
    return <Text>Error while channels loading!</Text>
  }

  if (isLoading) {
    return <Text>Channels are loading...</Text>
  }

  const renderChannelsListItem = ({ item }: { item: IChannel }) => {
    return <ChannelsListItem key={item.id} channel={item} />;
  };

  return (
    <FlatList
      data={channels ?? []}
      renderItem={renderChannelsListItem}
      keyExtractor={(item) => item.id}
    />
  );
};

export default ChannelsList;
