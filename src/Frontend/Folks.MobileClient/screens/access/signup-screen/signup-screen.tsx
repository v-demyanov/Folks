import { Button } from 'react-native';
import { View } from 'react-native';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { StyleSheet } from 'react-native';
import { MD3Theme, Text, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import { RootStackParamList } from '../../../navigation/app-navigator';
import AccessHeader from '../../../features/access/access-header/access-header';
import SignupForm from '../../../features/access/signup-form/signup-form';

const buildStyles = (theme: MD3Theme) =>
  StyleSheet.create({
    wrapper: {
      display: 'flex',
      flexDirection: 'column',
      height: '100%',
      backgroundColor: theme.colors.background,
    },
    textWrapper: {
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      paddingTop: 5,
      paddingBottom: 40,
      width: '100%',
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
  });

type Props = NativeStackScreenProps<RootStackParamList, 'Signup'>;

const SignupScreen = ({ navigation }: Props): JSX.Element => {
  const theme = useTheme();

  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={[styles.wrapper]}>
      <AccessHeader />
      <View style={[styles.textWrapper]}>
        <Text style={[styles.mainText]} variant="headlineLarge">
          Registration in Folks
        </Text>
        <Text style={[styles.secondaryText]} variant="titleSmall">
          After registration, wait for administrator confirmation to sign in
          Folks.
        </Text>
      </View>

      <SignupForm wrapperStyle={{ paddingHorizontal: 30 }} />
    </View>
  );
};

export default SignupScreen;
