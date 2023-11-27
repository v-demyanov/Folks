import { Appbar } from 'react-native-paper';
import { memo } from 'react';

import UserAccount from '../../users/components/user-account/user-account';
import useAuth from '../../auth/hooks/use-auth';
import { useAppDispatch } from '../../../common/hooks/store-hooks';
import { api } from '../../../api/api';

const HomeAppbar = (): JSX.Element => {
  const { signOutAsync } = useAuth();
  const dispatch = useAppDispatch();

  const handleLogoutButtonPress = async (): Promise<void> => {
    await signOutAsync();
    dispatch(api.util.resetApiState());
  };

  return (
    <Appbar.Header>
      <Appbar.Content title={<UserAccount />} />
      <Appbar.Action
        icon="logout"
        onPress={handleLogoutButtonPress}
      />
    </Appbar.Header>
  );
};

export default memo(HomeAppbar);
