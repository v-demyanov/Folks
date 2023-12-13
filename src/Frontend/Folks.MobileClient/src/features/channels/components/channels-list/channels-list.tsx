import { FlatList, View } from 'react-native';
import { useTheme } from 'react-native-paper';
import { useMemo } from 'react';
import { useNavigation } from '@react-navigation/native';

import ChannelsListItem from './channels-list-item/channels-list-item';
import IChannel from '../../models/channel';
import { useGetOwnChannelsQuery } from '../../api/channels.api';
import buildStyles from './channels-list.styles';
import { Theme } from '../../../../themes/types/theme';
import ChannelsListError from './channels-list-error/channels-list-error';
import ChannelsListEmptyResult from './channels-list-empty-result/channels-list-empty-result';
import { StackNavigation } from '../../../../navigation/app-navigator';

const ChannelsList = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const navigation = useNavigation<StackNavigation>();

  const { data: channels = [], isError: isChannelsQueryError } =
    useGetOwnChannelsQuery(null);

  const handleListItemPress = (group: IChannel): void => {
    navigation.navigate('Group', group);
  };

  const renderChannelsListItem = ({ item }: { item: IChannel }) => {
    return (
      <ChannelsListItem
        key={item.id}
        channel={item}
        onPress={handleListItemPress}
      />
    );
  };

  if (isChannelsQueryError) {
    return <ChannelsListError />;
  }

  if (!channels.length) {
    return <ChannelsListEmptyResult />;
  }

  return (
    <FlatList
      data={channels}
      renderItem={renderChannelsListItem}
      keyExtractor={(item) => item.id}
      ItemSeparatorComponent={() => <View style={[styles.itemSeparator]} />}
    />
  );
};

export default ChannelsList;
