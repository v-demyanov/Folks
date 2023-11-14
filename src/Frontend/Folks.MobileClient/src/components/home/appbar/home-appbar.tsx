import { Appbar } from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';

import { StackNavigation } from '../../../navigation/app-navigator';
import UserAccount from '../../../features/users/user-account/user-account';
import useAuth from '../../../features/auth/hooks/use-auth';

const HomeAppbar = (): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();
  const { signOutAsync } = useAuth();

  const handleLogoutButtonPress = async (): Promise<void> => {
    await signOutAsync();
    navigation.navigate('Welcome');
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

export default HomeAppbar;
