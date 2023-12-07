import { useEffect, useState } from 'react';
import { FormikProps, useFormik } from 'formik';

import CreateGroupForm from '../../../features/groups/components/create-group/create-group-form/create-group-form';
import CreateGroupHeader from '../../../features/groups/components/create-group/create-group-header/create-group-header';
import SelectableUsersList from '../../../features/users/components/selectable-users-list/selectable-users-list';
import SelectedUsersChips from '../../../features/users/components/selected-users-chips/selected-users-chips';
import CreateGroupButton from '../../../features/groups/components/create-group/create-group-button/create-group-button';
import { useGetAllUsersQuery } from '../../../features/users/api/users.api';
import ISelectableUser from '../../../features/users/models/selectable-user';
import useAuth from '../../../features/auth/hooks/use-auth';
import ICreateGroupFormValue from '../../../features/groups/models/create-group-form-value';
import { useCreateGroupMutation } from '../../../features/groups/api/groups.api';
import ICreateGroupCommand from '../../../features/groups/models/create-group-command';
import CreateGroupFormValidationSchema from '../../../features/groups/validation/create-group-form.validation';

const CreateGroupScreen = (): JSX.Element => {
  const [selectableUsers, setSelectableUsers] = useState<ISelectableUser[]>([]);
  const { currentUser } = useAuth();
  const { data: users = [], isSuccess: isUsersQuerySuccess } =
    useGetAllUsersQuery(null, { refetchOnMountOrArgChange: true });
  const [createGroup, { isLoading: isCreatingGroup }] =
    useCreateGroupMutation();

  const createGroupForm: FormikProps<ICreateGroupFormValue> =
    useFormik<ICreateGroupFormValue>({
      initialValues: {},
      enableReinitialize: true,
      validationSchema: CreateGroupFormValidationSchema,
      onSubmit: (value: ICreateGroupFormValue) => {
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
    });

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
      <CreateGroupHeader />
      <CreateGroupForm form={createGroupForm} />
      <SelectedUsersChips
        users={selectableUsers}
        onChipClose={handleChipClose}
      />
      <SelectableUsersList
        users={selectableUsers}
        onListItemPress={handleListItemPress}
      />
      <CreateGroupButton
        onPress={createGroupForm.handleSubmit}
        disabled={
          !createGroupForm.isValid || !createGroupForm.dirty || isCreatingGroup
        }
      />
    </>
  );
};

export default CreateGroupScreen;
