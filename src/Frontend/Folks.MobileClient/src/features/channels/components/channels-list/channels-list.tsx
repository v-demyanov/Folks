import { FlatList, View } from 'react-native';
import { useTheme } from 'react-native-paper';
import { useMemo } from 'react';
import { useNavigation } from '@react-navigation/native';

import ChannelsListItem from './channels-list-item/channels-list-item';
import { IChannel } from '../../models';
import { useGetOwnChannelsQuery } from '../../api/channels.api';
import buildStyles from './channels-list.styles';
import { Theme } from '../../../../themes/types/theme';
import ChannelsListEmpty from './channels-list-empty/channels-list-empty';
import { StackNavigation } from '../../../../navigation/app-navigator';
import { InformationContainer } from '../../../../common/components';

const ChannelsList = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const navigation = useNavigation<StackNavigation>();

  const { data: channels = [], isError: isGetChannelsError } =
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

  function getChannelsErrorMessage(): string {
    return 'Oops! Something went wrong,\n while channels loading.';
  }

  if (isGetChannelsError) {
    return <InformationContainer message={getChannelsErrorMessage()} />;
  }

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
