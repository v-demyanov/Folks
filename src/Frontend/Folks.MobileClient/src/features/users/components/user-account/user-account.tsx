import { useMemo } from 'react';
import { View } from 'react-native';
import { Text, Avatar, useTheme } from 'react-native-paper';

import buildStyles from './user-account.styles';
import { IconsConstants } from '../../../../common';
import { Theme } from '../../../../themes/types/theme';
import { useAuth } from '../../../auth/hooks';

const UserAccount = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);
  const { currentUser } = useAuth();

  return (
    <View style={[styles.view]}>
      <Avatar.Icon
        style={[styles.avatarIcon]}
        size={IconsConstants.USER_ACCOUNT_IMAGE_SIZE}
        icon="account"
      />
      <Text variant="titleMedium" style={[styles.userName]}>
        {currentUser?.name}
      </Text>
    </View>
  );
};

export default UserAccount;
