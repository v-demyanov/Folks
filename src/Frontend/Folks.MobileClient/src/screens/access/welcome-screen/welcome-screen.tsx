import { View, Image } from 'react-native';
import { useTheme, Text, Button } from 'react-native-paper';
import { useMemo, useState } from 'react';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { ActivityIndicator } from 'react-native-paper';

import Logo from '../../../components/logo/logo';
import { RootStackParamList } from '../../../navigation/app-navigator';
import useAuth from '../../../features/auth/hooks/use-auth';
import SignInResult from '../../../features/auth/models/auth-result-type';
import buildStyles from './welcome-screen-styles';

type Props = NativeStackScreenProps<RootStackParamList, 'Welcome'>;

const WelcomeScreen = ({ navigation }: Props): JSX.Element => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const theme = useTheme();
  const styles = useMemo(() => buildStyles(theme), [theme]);
  const { signInAsync, authRequest } = useAuth();

  const handleSignInButtonPressAsync = async (): Promise<void> => {
    setIsLoading(true);
    const signInResult = await signInAsync();
    setIsLoading(false);

    if (signInResult === SignInResult.Success) {
      navigation.navigate('Home');
    }
  };

  if (isLoading) {
    return (
      <ActivityIndicator
        animating={true}
        size="large"
        style={{ width: '100%', height: '100%' }}
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
        <Button mode="elevated">Sign Up</Button>
      </View>
    </View>
  );
};

export default WelcomeScreen;
