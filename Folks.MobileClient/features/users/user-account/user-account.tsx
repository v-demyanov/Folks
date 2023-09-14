import { View } from 'react-native';
import { Text } from 'react-native-paper';
import { Avatar } from 'react-native-paper';
import { StyleSheet } from 'react-native';

import { USER_ACCOUNT_ICON_SIZE } from '../../../common/constants/icons.constants';

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
  return (
    <View style={[styles.view]}>
      <Avatar.Icon
        style={[styles.avatarIcon]}
        size={USER_ACCOUNT_ICON_SIZE}
        icon="account"
      />
      <Text>Vladislav Demyanov</Text>
    </View>
  );
};

export default UserAccount;
