import { View, ViewStyle } from 'react-native';
import { Button, MD3Theme, TextInput, useTheme } from 'react-native-paper';
import { StackNavigation } from '../../../navigation/app-navigator';
import { useNavigation } from '@react-navigation/native';
import { StyleSheet } from 'react-native';
import { useMemo } from 'react';

const buildStyles = (theme: MD3Theme) =>
  StyleSheet.create({
    textInput: {
      marginBottom: 20,
    },
  });

export interface ISignupFormProps {
  wrapperStyle?: ViewStyle;
}

const SignupForm = (props: ISignupFormProps): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();
  const theme = useTheme();

  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={props.wrapperStyle}>
      <TextInput style={[styles.textInput]} label="Email" />
      <TextInput style={[styles.textInput]} label="Username" />
      <TextInput
        style={[styles.textInput]}
        label="Password"
        secureTextEntry
        right={<TextInput.Icon icon="eye" />}
      />
      <TextInput
        style={[styles.textInput]}
        label="Confirm your password"
        secureTextEntry
        right={<TextInput.Icon icon="eye" />}
      />

      <Button onPress={() => navigation.navigate('Signin')} mode="contained">
        Sing Up
      </Button>
    </View>
  );
};

export default SignupForm;
