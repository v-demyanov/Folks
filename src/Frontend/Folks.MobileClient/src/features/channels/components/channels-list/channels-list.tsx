import { FlatList, View } from 'react-native';
import { Text, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import ChannelsListItem from './channels-list-item/channels-list-item';
import IChannel from '../../models/channel';
import { useGetOwnChannelsQuery } from '../../api/channels.api';
import buildStyles from './channels-list.styles';

const ChannelsList = (): JSX.Element => {
  const theme = useTheme();
  const { data: channels, error } = useGetOwnChannelsQuery(null);

  const styles = useMemo(() => buildStyles(theme), [theme]);

  if (error) {
    return (
      <View style={[styles.centeredView]}>
        <Text variant="labelMedium">Oops! Something went wrong...</Text>
      </View>
    );
  }

  const renderChannelsListItem = ({ item }: { item: IChannel }) => {
    return <ChannelsListItem key={item.id} channel={item} />;
  };

  return (
    <>
      {channels?.length ? (
        <FlatList
          data={channels}
          renderItem={renderChannelsListItem}
          keyExtractor={(item) => item.id}
          ItemSeparatorComponent={() => <View style={[styles.itemSeparator]} />}
        />
      ) : (
        <View style={[styles.centeredView]}>
          <Text variant="labelMedium">Create your first group or chat!</Text>
        </View>
      )}
    </>
  );
};

export default ChannelsList;
