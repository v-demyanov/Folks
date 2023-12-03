import { useEffect, useState } from 'react';
import { FormikProps, useFormik } from 'formik';

import NewGroupForm from '../../../features/groups/components/new-group/new-group-form/new-group-form';
import NewGroupHeader from '../../../features/groups/components/new-group/new-group-header/new-group-header';
import SelectableUsersList from '../../../features/users/components/selectable-users-list/selectable-users-list';
import SelectedUsersChips from '../../../features/users/components/selected-users-chips/selected-users-chips';
import GroupCreateButton from '../../../features/groups/components/new-group/group-create-button/group-create-button';
import { useGetAllUsersQuery } from '../../../features/users/api/users.api';
import ISelectableUser from '../../../features/users/models/selectable-user';
import useAuth from '../../../features/auth/hooks/use-auth';
import IGroupFormValue from '../../../features/groups/models/group-form-value';
import { useCreateGroupMutation } from '../../../features/groups/api/groups.api';
import ICreateGroupCommand from '../../../features/groups/models/group-create-command';

const NewGroupScreen = (): JSX.Element => {
  const [selectableUsers, setSelectableUsers] = useState<ISelectableUser[]>([]);
  const { currentUser } = useAuth();
  const { data: users = [], isSuccess: isUsersQuerySuccess } =
    useGetAllUsersQuery(null, { refetchOnMountOrArgChange: true });
  const [createGroup, { isLoading: isCreatingGroup }] =
    useCreateGroupMutation();

  const newGroupForm: FormikProps<IGroupFormValue> = useFormik<IGroupFormValue>(
    {
      initialValues: {},
      enableReinitialize: true,
      onSubmit: (value: IGroupFormValue) => {
        if (!currentUser) {
          return;
        }

        const userIds = selectableUsers
          .filter((user) => user.isSelected)
          .map((user) => user.id);

        userIds.push(currentUser.sub);

        createGroup({
          title: value.title,
          userIds,
        } as ICreateGroupCommand)
          .unwrap()
          .then((createdGroup) => {
            console.log('createdGroup: ', createdGroup);
          });
      },
    }
  );

  useEffect(() => {
    const selectableUsers = prepareSelecatableUsers();
    setSelectableUsers(selectableUsers);
  }, [isUsersQuerySuccess]);

  const handleListItemPress = (user: ISelectableUser) => {
    user.isSelected = !user.isSelected;
    setSelectableUsers([...selectableUsers]);
  };

  const handleChipClose = (user: ISelectableUser) => {
    user.isSelected = false;
    setSelectableUsers([...selectableUsers]);
  };

  function prepareSelecatableUsers(): ISelectableUser[] {
    return users
      .filter((user) => user.id !== currentUser?.sub)
      .map((user) => ({
        ...user,
        isSelected: false,
        status: 'last seen recently',
      }));
  }

  return (
    <>
      <NewGroupHeader />
      <NewGroupForm form={newGroupForm} />
      <SelectedUsersChips
        users={selectableUsers}
        onChipClose={handleChipClose}
      />
      <SelectableUsersList
        users={selectableUsers}
        onListItemPress={handleListItemPress}
      />
      <GroupCreateButton
        onPress={newGroupForm.handleSubmit}
        disabled={
          !newGroupForm.isValid || !newGroupForm.dirty || isCreatingGroup
        }
      />
    </>
  );
};

export default NewGroupScreen;
