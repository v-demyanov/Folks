import { useState } from 'react';

import NewGroupForm from '../../../features/groups/components/new-group/new-group-form/new-group-form';
import NewGroupHeader from '../../../features/groups/components/new-group/new-group-header/new-group-header';
import SelectableUsersList from '../../../features/users/components/selectable-users-list/selectable-users-list';
import IUser from '../../../features/users/models/user';
import SelectedUsersChips from '../../../features/users/components/selected-users-chips/selected-users-chips';
import GroupCreateButton from '../../../features/groups/components/new-group/group-create-button/group-create-button';

const NewGroupScreen = (): JSX.Element => {
  const [users, setUsers] = useState(
    [...Array(20).keys()].map((x) => ({
      id: x.toString(),
      title: `User ${x}`,
      selected: false,
      status: 'last seen recently',
    }))
  );

  const handleListItemPress = (user: IUser) => {
    user.selected = !user.selected;
    setUsers([...users]);
  };

  const handleChipClose = (user: IUser) => {
    user.selected = false;
    setUsers([...users]);
  };

  return (
    <>
      <NewGroupHeader />
      <NewGroupForm users={users} />
      <SelectedUsersChips users={users} onChipClose={handleChipClose} />
      <SelectableUsersList
        users={users}
        onListItemPress={handleListItemPress}
      />
      <GroupCreateButton />
    </>
  );
};

export default NewGroupScreen;
