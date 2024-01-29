import { useMemo, useState } from 'react';
import { View, Image } from 'react-native';
import { useTheme, Text, Button, ActivityIndicator } from 'react-native-paper';

import buildStyles from './welcome-screen-styles';
import { Logo } from '../../../common/components';
import useAuth from '../../../features/auth/hooks/use-auth';
import { channelsHubConnection } from '../../../features/signalr/connections';
import { Theme } from '../../../themes/types/theme';

const WelcomeScreen = (): JSX.Element => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);
  const { signInAsync, authRequest } = useAuth();

  const handleSignInButtonPressAsync = async (): Promise<void> => {
    setIsLoading(true);
    signInAsync()
      .then(async () => {
        await channelsHubConnection.start();
      })
      .finally(() => setIsLoading(false));
  };

  if (isLoading) {
    return (
      <ActivityIndicator
        animating
        size="large"
        style={[styles.activityIndicator]}
      />
    );
  }

  return (
    <View style={[styles.wrappper]}>
      <Logo
        fill={theme.colors.onBackground}
        width={250}
        height={100}
        style={[styles.logo]}
      />
      <Image
        source={require('./../../../../assets/welcome.png')}
        style={[styles.image]}
      />
      <View style={[styles.welcomeMessagesWrapper]}>
        <Text style={[styles.mainText]} variant="headlineLarge">
          Welcome to Folks
        </Text>
        <Text style={[styles.secondaryText]} variant="titleSmall">
          This application provides messaging, audio and video calls, but only
          for family and friends.
        </Text>
      </View>

      <View style={[styles.actionsWrapper]}>
        <Button
          mode="contained"
          disabled={!authRequest}
          onPress={handleSignInButtonPressAsync}
        >
          Sign In
        </Button>
      </View>
    </View>
  );
};

export default WelcomeScreen;
