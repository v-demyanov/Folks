import { Appbar } from 'react-native-paper';
import { memo } from 'react';

import UserAccount from '../../../features/users/components/user-account/user-account';
import useAuth from '../../../features/auth/hooks/use-auth';

const HomeAppbar = (): JSX.Element => {
  const { signOutAsync } = useAuth();

  const handleLogoutButtonPress = async (): Promise<void> => {
    await signOutAsync();
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
