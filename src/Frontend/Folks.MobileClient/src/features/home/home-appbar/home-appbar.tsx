import { Appbar, useTheme } from 'react-native-paper';
import { memo, useMemo } from 'react';
import { View } from 'react-native';

import UserAccount from '../../users/components/user-account/user-account';
import useAuth from '../../auth/hooks/use-auth';
import { useAppDispatch } from '../../../common/hooks/store-hooks';
import { api } from '../../../api/api';
import buildStyles from './home-appbar.styles';
import ChannelsLoadingIndicator from '../../channels/components/channels-loading-indicator/channels-loading-indicator';
import { Theme } from '../../../themes/types/theme';

const HomeAppbar = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);
  const { signOutAsync } = useAuth();
  const dispatch = useAppDispatch();

  const handleLogoutButtonPress = async (): Promise<void> => {
    await signOutAsync();
    dispatch(api.util.resetApiState());
  };

  return (
    <Appbar.Header style={[styles.header]}>
      <Appbar.Content
        title={
          <View style={[styles.titleView]}>
            <UserAccount />
            <ChannelsLoadingIndicator />
            <View></View>
          </View>
        }
      />
      <Appbar.Action
        icon="logout"
        color={theme.colors.onPrimary}
        onPress={handleLogoutButtonPress}
      />
    </Appbar.Header>
  );
};

export default memo(HomeAppbar);
