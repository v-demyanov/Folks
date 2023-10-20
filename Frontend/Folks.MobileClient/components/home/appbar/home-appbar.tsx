import { Appbar } from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';

import { StackNavigation } from '../../../navigation/app-navigator';
import UserAccount from '../../../features/users/user-account/user-account';

const HomeAppbar = (): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();

  return (
    <Appbar.Header>
      <Appbar.Content title={<UserAccount />} />
      <Appbar.Action
        icon="logout"
        onPress={() => navigation.navigate('Signin')}
      />
    </Appbar.Header>
  );
};

export default HomeAppbar;
