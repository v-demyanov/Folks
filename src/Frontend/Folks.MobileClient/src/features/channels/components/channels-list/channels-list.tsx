import { FlatList, View } from 'react-native';
import { useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import ChannelsListItem from './channels-list-item/channels-list-item';
import { IChannel, IChannelListProps } from '../../models';
import buildStyles from './channels-list.styles';
import { Theme } from '../../../../themes/types/theme';
import ChannelsListEmpty from './channels-list-empty/channels-list-empty';
import { SelectableItem } from '../../../../common/models';

const ChannelsList = ({
  channels,
  onListItemPress,
  onListItemPressIn,
  onListItemPressOut,
}: IChannelListProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const renderChannelsListItem = ({
    item,
  }: {
    item: SelectableItem<IChannel>;
  }) => {
    return (
      <ChannelsListItem
        key={item.id}
        channel={item}
        onPress={onListItemPress}
        onPressIn={onListItemPressIn}
        onPressOut={onListItemPressOut}
      />
    );
  };

  if (!channels.length) {
    return <ChannelsListEmpty />;
  }

  return (
    <FlatList
      style={[styles.flatList]}
      data={channels}
      renderItem={renderChannelsListItem}
      keyExtractor={(item) => item.id}
      ItemSeparatorComponent={() => <View style={[styles.itemSeparator]} />}
    />
  );
};

export default ChannelsList;
