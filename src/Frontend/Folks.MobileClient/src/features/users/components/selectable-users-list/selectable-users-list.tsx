import { FlatList, View } from 'react-native';
import { useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import buildStyles from './selectable-users-list.styles';
import SelectableUsersListItem from './selectable-users-list-item/selectable-users-list-item';
import ISelectableUsersListProps from '../../models/selectable-users-list.props';
import { Theme } from '../../../../themes/types/theme';
import ISelectableUser from '../../models/selectable-user';
import SelectableUsersListEmpty from './selectable-users-list-empty/selectable-users-list-empty';
import { useGetAllUsersQuery } from '../../api/users.api';
import SelectableUsersListError from './selectable-users-list-error/selectable-users-list-error';

const SelectableUsersList = ({
  users,
  onListItemPress,
}: ISelectableUsersListProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const { isError: isUsersQueryError } = useGetAllUsersQuery(null);

  const renderListItem = ({ item }: { item: ISelectableUser }) => {
    return (
      <SelectableUsersListItem
        key={item.id}
        user={item}
        onPress={onListItemPress}
      />
    );
  };

  if (isUsersQueryError) {
    return <SelectableUsersListError />;
  }

  return (
    <FlatList
      style={[styles.flatList]}
      data={users}
      renderItem={renderListItem}
      keyExtractor={(item) => item.id}
      ItemSeparatorComponent={() => <View style={[styles.itemSeparator]} />}
      ListEmptyComponent={<SelectableUsersListEmpty />}
    />
  );
};

export default SelectableUsersList;
