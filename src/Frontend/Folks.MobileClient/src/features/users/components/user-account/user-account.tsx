import { View, StyleSheet } from 'react-native';
import { Text, Avatar } from 'react-native-paper';

import { USER_ACCOUNT_IMAGE_SIZE } from '../../../../common/constants/icons.constants';
import useAuth from '../../../auth/hooks/use-auth';

const styles = StyleSheet.create({
  view: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center',
  },
  avatarIcon: {
    marginRight: 6,
  },
});

const UserAccount = (): JSX.Element => {
  const { currentUser } = useAuth();

  return (
    <View style={[styles.view]}>
      <Avatar.Icon
        style={[styles.avatarIcon]}
        size={USER_ACCOUNT_IMAGE_SIZE}
        icon="account"
      />
      <Text>{currentUser?.name}</Text>
    </View>
  );
};

export default UserAccount;
