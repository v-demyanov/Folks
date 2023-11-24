import { FlatList } from 'react-native';

import ChannelsListItem from './channels-list-item/channels-list-item';

const ChannelsList = (): JSX.Element => {
  const renderChannelsListItem = () => {
    return <ChannelsListItem key={''} />;
  };

  return (
    <FlatList
      data={[]}
      renderItem={renderChannelsListItem}
      keyExtractor={() => ''}
    />
  );
};

export default ChannelsList;
