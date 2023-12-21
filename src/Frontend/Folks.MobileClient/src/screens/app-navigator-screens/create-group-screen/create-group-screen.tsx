import { useEffect, useState } from 'react';
import { FormikProps, useFormik } from 'formik';
import { useNavigation } from '@react-navigation/native';
import { Snackbar } from 'react-native-paper';

import {
  CreateGroupButton,
  CreateGroupForm,
  CreateGroupHeader,
} from '../../../features/groups/components';
import {
  CreateGroupFormValidationSchema,
  useCreateGroupMutation,
} from '../../../features/groups';
import {
  ICreateGroupFormValue,
  ICreateGroupCommand,
} from '../../../features/groups/models';
import { useGetAllUsersQuery } from '../../../features/users';
import { ISelectableUser } from '../../../features/users/models';
import {
  SelectableUsersList,
  SelectedUsersChips,
} from '../../../features/users/components';
import { useAuth } from '../../../features/auth/hooks';
import { useArrayEffect } from '../../../common/hooks';
import { InformationContainer } from '../../../common/components';
import { StackNavigation } from '../../../navigation/app-navigator';

const CreateGroupScreen = (): JSX.Element => {
  const { currentUser } = useAuth();
  const navigation = useNavigation<StackNavigation>();

  const { data: users = [], isError: isGetAllUsersError } = useGetAllUsersQuery(
    null,
    {
      refetchOnMountOrArgChange: true,
    }
  );
  const [
    createGroup,
    { isLoading: isCreatingGroup, isError: isCreateGroupError },
  ] = useCreateGroupMutation();

  const [selectableUsers, setSelectableUsers] = useState<ISelectableUser[]>([]);
  const [createGroupErrorVisible, setCreateGroupErrorVisible] = useState(false);

  useArrayEffect(() => {
    const selectableUsers = prepareSelecatableUsers();
    setSelectableUsers(selectableUsers);
  }, [users, currentUser]);

  useEffect(() => {
    setCreateGroupErrorVisible(isCreateGroupError);
  }, [isCreateGroupError]);

  function prepareSelecatableUsers(): ISelectableUser[] {
    if (!currentUser) {
      return [];
    }

    return users
      .filter((user) => user.id !== currentUser.sub)
      .map((user) => ({
        ...user,
        isSelected: false,
        status: 'last seen recently',
      }));
  }

  const createGroupForm: FormikProps<ICreateGroupFormValue> =
    useFormik<ICreateGroupFormValue>({
      initialValues: {},
      enableReinitialize: true,
      validationSchema: CreateGroupFormValidationSchema,
      onSubmit: async (value: ICreateGroupFormValue) => {
        const createGroupCommand = prepareCreateGroupCommand(value);
        if (!createGroupCommand) {
          return;
        }

        const group = await createGroup(createGroupCommand).unwrap();
        resetCreateGroupForm();
        navigation.navigate('Group', group);
      },
    });

  const resetCreateGroupForm = (): void => {
    createGroupForm.resetForm();
    const selectableUsers = prepareSelecatableUsers();
    setSelectableUsers(selectableUsers);
  };

  const handleListItemPress = (user: ISelectableUser) => {
    user.isSelected = !user.isSelected;
    setSelectableUsers([...selectableUsers]);
  };

  const handleChipClose = (user: ISelectableUser) => {
    user.isSelected = false;
    setSelectableUsers([...selectableUsers]);
  };

  function prepareCreateGroupCommand(
    formValue: ICreateGroupFormValue
  ): ICreateGroupCommand | null {
    if (!currentUser) {
      return null;
    }

    const userIds = selectableUsers
      .filter((user) => user.isSelected)
      .map((user) => user.id);
    userIds.push(currentUser.sub);

    return {
      title: formValue.title,
      userIds,
    } as ICreateGroupCommand;
  }

  function getGetAllUsersErrorMessage(): string {
    return 'Oops! Something went wrong,\n while users loading.';
  }

  function getCreateGroupErrorMessage(): string {
    return 'Oops! Something went wrong, while group creating.';
  }

  return (
    <>
      <CreateGroupHeader />
      <CreateGroupForm form={createGroupForm} />
      <SelectedUsersChips
        users={selectableUsers}
        onChipClose={handleChipClose}
      />
      {isGetAllUsersError ? (
        <InformationContainer message={getGetAllUsersErrorMessage()} />
      ) : (
        <SelectableUsersList
          users={selectableUsers}
          onListItemPress={handleListItemPress}
        />
      )}
      <CreateGroupButton
        onPress={createGroupForm.handleSubmit}
        disabled={
          !createGroupForm.isValid || !createGroupForm.dirty || isCreatingGroup
        }
      />
      <Snackbar
        visible={createGroupErrorVisible}
        onDismiss={() => setCreateGroupErrorVisible(false)}
        action={{
          label: 'Close',
          onPress: () => setCreateGroupErrorVisible(false),
        }}
      >
        {getCreateGroupErrorMessage()}
      </Snackbar>
    </>
  );
};

export default CreateGroupScreen;
