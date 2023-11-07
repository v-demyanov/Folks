import { View, Image, StyleSheet } from 'react-native';
import { useTheme, Text, Button, MD3Theme } from 'react-native-paper';
import { useEffect, useMemo } from 'react';
import * as WebBrowser from 'expo-web-browser';

import Logo from '../../../components/logo/logo';
import {
  exchangeCodeAsync,
  makeRedirectUri,
  useAuthRequest,
  useAutoDiscovery,
} from 'expo-auth-session';

const buildStyles = (theme: MD3Theme) =>
  StyleSheet.create({
    wrappper: {
      backgroundColor: theme.colors.background,
      height: '100%',
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center',
    },
    logo: {
      paddingBottom: 10,
    },
    image: {
      width: 300,
      height: 250,
    },
    welcomeMessagesWrapper: {
      display: 'flex',
      justifyContent: 'center',
      paddingTop: 10,
    },
    mainText: {
      textAlign: 'center',
      fontWeight: 'bold',
    },
    secondaryText: {
      textAlign: 'center',
      color: theme.colors.onSurfaceVariant,
      maxWidth: 320,
    },
    actionsWrapper: {
      display: 'flex',
      flexDirection: 'row',
      paddingBottom: 20,
      paddingTop: 40,
      columnGap: 10,
    },
  });

WebBrowser.maybeCompleteAuthSession();
const redirectUri = makeRedirectUri();

const WelcomeScreen = (): JSX.Element => {
  const theme = useTheme();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const discovery = useAutoDiscovery('http://192.168.0.104:8002');

  const [request, response, promptAsync] = useAuthRequest(
    {
      clientId: 'native.code',
      redirectUri,
      scopes: ['openid', 'profile'],
    },
    discovery
  );

  useEffect(() => {
    if (!response || !discovery || response.type !== 'success') {
      return;
    }

    exchangeCodeAsync(
      {
        clientId: 'native.code',
        code: response.params.code,
        redirectUri: redirectUri,
        extraParams: {
          code_verifier: request?.codeVerifier || '',
        },
      },
      discovery
    ).then((tokenResponse) => console.log(tokenResponse));
  }, [response]);

  return (
    <View style={[styles.wrappper]}>
      <Logo
        fill={theme.colors.onBackground}
        width={250}
        height={100}
        style={[styles.logo]}
      />
      <Image
        source={require('./.../../../../../assets/welcome.png')}
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
          disabled={!request}
          onPress={() => promptAsync()}
        >
          Sign In
        </Button>
        <Button mode="elevated">Sign Up</Button>
      </View>
    </View>
  );
};

export default WelcomeScreen;
