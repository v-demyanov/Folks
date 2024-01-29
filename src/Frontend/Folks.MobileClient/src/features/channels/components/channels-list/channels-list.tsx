import { useMemo } from 'react';
import { FlatList, View } from 'react-native';
import { useTheme } from 'react-native-paper';

import ChannelsListEmpty from './channels-list-empty/channels-list-empty';
import ChannelsListItem from './channels-list-item/channels-list-item';
import buildStyles from './channels-list.styles';
import { SelectableItem } from '../../../../common/models';
import { Theme } from '../../../../themes/types/theme';
import { IChannel, IChannelListProps } from '../../models';

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
