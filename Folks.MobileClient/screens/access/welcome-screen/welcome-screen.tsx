import { View, Image } from 'react-native';
import { useTheme, Text, Button, MD3Theme } from 'react-native-paper';
import { StyleSheet } from 'react-native';
import { useMemo } from 'react';

import Logo from '../../../components/logo/logo';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { RootStackParamList } from '../../../navigation/app-navigator';

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

type Props = NativeStackScreenProps<RootStackParamList, 'Welcome'>;

const WelcomeScreen = ({ navigation }: Props): JSX.Element => {
  const theme = useTheme();

  const styles = useMemo(() => buildStyles(theme), [theme]);

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
        <Button mode="contained" onPress={() => navigation.navigate('Signin')}>
          Sign In
        </Button>
        <Button mode="elevated" onPress={() => navigation.navigate('Signup')}>
          Sign Up
        </Button>
      </View>
    </View>
  );
};

export default WelcomeScreen;
