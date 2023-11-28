import { FlatList, View } from 'react-native';
import { useTheme } from 'react-native-paper';
import { useMemo, useState } from 'react';

import buildStyles from './selectable-users-list.styles';
import SelectableUsersListItem from './selectable-users-list-item/selectable-users-list-item';
import IUser from '../../models/user';
import ISelectableUsersListProps from '../../models/selectable-users-list.props';
import { Theme } from '../../../../themes/types/theme';

const SelectableUsersList = ({
  users,
  onListItemPress,
}: ISelectableUsersListProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const renderListItem = ({ item }: { item: IUser }) => {
    return (
      <SelectableUsersListItem
        key={item.id}
        user={item}
        onPress={onListItemPress}
      />
    );
  };

  return (
    <FlatList
      style={[styles.flatList]}
      data={users}
      renderItem={renderListItem}
      keyExtractor={(item) => item.id}
      ItemSeparatorComponent={() => <View style={[styles.itemSeparator]} />}
    />
  );
};

export default SelectableUsersList;
