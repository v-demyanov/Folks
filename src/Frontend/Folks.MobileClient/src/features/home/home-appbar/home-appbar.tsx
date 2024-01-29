import { memo, useMemo } from 'react';
import { View } from 'react-native';
import { Appbar, useTheme } from 'react-native-paper';

import buildStyles from './home-appbar.styles';
import { api } from '../../../api/api';
import { useAppDispatch } from '../../../common/hooks';
import { Theme } from '../../../themes/types/theme';
import { useAuth } from '../../auth/hooks';
import { ChannelsLoadingIndicator } from '../../channels/components';
import { channelsHubConnection } from '../../signalr/connections';
import { UserAccount } from '../../users/components';

const HomeAppbar = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);
  const { signOutAsync } = useAuth();
  const dispatch = useAppDispatch();

  const handleLogoutButtonPress = async (): Promise<void> => {
    await signOutAsync();
    await channelsHubConnection.stop();
    dispatch(api.util.resetApiState());
  };

  return (
    <Appbar.Header style={[styles.header]}>
      <Appbar.Content
        title={
          <View style={[styles.titleView]}>
            <UserAccount />
            <ChannelsLoadingIndicator />
            <View />
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
