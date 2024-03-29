import { useMemo } from 'react';
import { FlatList, View } from 'react-native';
import { useTheme } from 'react-native-paper';

import SelectableUsersListEmpty from './selectable-users-list-empty/selectable-users-list-empty';
import SelectableUsersListItem from './selectable-users-list-item/selectable-users-list-item';
import buildStyles from './selectable-users-list.styles';
import { SelectableItem } from '../../../../common/models';
import { Theme } from '../../../../themes/types/theme';
import { ISelectableUsersListProps, IUser } from '../../models';

const SelectableUsersList = ({
  items: users,
  onListItemPress,
}: ISelectableUsersListProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const renderListItem = ({ item }: { item: SelectableItem<IUser> }) => {
    return (
      <SelectableUsersListItem
        key={item.id}
        item={item}
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
