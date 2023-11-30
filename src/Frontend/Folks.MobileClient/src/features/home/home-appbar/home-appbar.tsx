import { Appbar } from 'react-native-paper';
import { memo } from 'react';
import { View } from 'react-native';

import UserAccount from '../../users/components/user-account/user-account';
import useAuth from '../../auth/hooks/use-auth';
import { useAppDispatch } from '../../../common/hooks/store-hooks';
import { api } from '../../../api/api';
import buildStyles from './home-appbar.styles';
import ChannelsLoadingIndicator from '../../channels/components/channels-loading-indicator/channels-loading-indicator';

const HomeAppbar = (): JSX.Element => {
  const styles = buildStyles();
  const { signOutAsync } = useAuth();
  const dispatch = useAppDispatch();

  const handleLogoutButtonPress = async (): Promise<void> => {
    await signOutAsync();
    dispatch(api.util.resetApiState());
  };

  return (
    <Appbar.Header>
      <Appbar.Content
        title={
          <View style={[styles.titleView]}>
            <UserAccount />
            <ChannelsLoadingIndicator />
            <View></View>
          </View>
        }
      />
      <Appbar.Action icon="logout" onPress={handleLogoutButtonPress} />
    </Appbar.Header>
  );
};

export default memo(HomeAppbar);
