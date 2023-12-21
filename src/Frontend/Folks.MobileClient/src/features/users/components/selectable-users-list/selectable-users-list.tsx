import { FlatList, View } from 'react-native';
import { useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import buildStyles from './selectable-users-list.styles';
import SelectableUsersListItem from './selectable-users-list-item/selectable-users-list-item';
import { Theme } from '../../../../themes/types/theme';
import { ISelectableUsersListProps, ISelectableUser } from '../../models';
import SelectableUsersListEmpty from './selectable-users-list-empty/selectable-users-list-empty';

const SelectableUsersList = ({
  users,
  onListItemPress,
}: ISelectableUsersListProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const renderListItem = ({ item }: { item: ISelectableUser }) => {
    return (
      <SelectableUsersListItem
        key={item.id}
        user={item}
        onPress={onListItemPress}
      />
    );
  };

  if (!users.length) {
    return <SelectableUsersListEmpty />;
  }

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
