import { View, Image, StyleSheet } from 'react-native';
import { useTheme, Text, Button, MD3Theme } from 'react-native-paper';
import { useEffect, useMemo } from 'react';
import * as WebBrowser from 'expo-web-browser';
import {
  exchangeCodeAsync,
  makeRedirectUri,
  useAuthRequest,
  useAutoDiscovery,
} from 'expo-auth-session';
import { NativeStackScreenProps } from '@react-navigation/native-stack';

import Logo from '../../../components/logo/logo';
import { RootStackParamList } from '../../../navigation/app-navigator';
import buildStyles from './welcome-screen-styles';

type Props = NativeStackScreenProps<RootStackParamList, 'Welcome'>;

const WelcomeScreen = ({ navigation }: Props): JSX.Element => {
  const theme = useTheme();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const discovery = useAutoDiscovery('http://192.168.0.104:8001');

  const [request, response, promptAsync] = useAuthRequest(
    {
      clientId: 'native.code',
      redirectUri,
      scopes: ['openid', 'profile', 'chatServiceApi'],
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
    ).then(async (tokenResponse) => {
      console.log(tokenResponse.accessToken);
      fetch('http://192.168.0.104:8000/chatservice/weatherforecast', {
        headers: { Authorization: `Bearer ${tokenResponse.accessToken}` },
      })
        .then((resp) => resp.json())
        .then((json) => console.log(json));
    });
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
        source={require('./../../../../assets/welcome.png')} // ./../../../../../../assets/welcome.png
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
        <Button mode="elevated" onPress={() => navigation.navigate('Home')}>
          Sign Up
        </Button>
      </View>
    </View>
  );
};

export default WelcomeScreen;
