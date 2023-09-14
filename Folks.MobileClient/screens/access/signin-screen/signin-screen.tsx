import { Button } from 'react-native';
import { View, Text } from 'react-native';
import { NativeStackScreenProps } from '@react-navigation/native-stack';

import { RootStackParamList } from '../../../navigation/app-navigator';

type Props = NativeStackScreenProps<RootStackParamList, 'Signin'>;

const SigninScreen = ({ navigation }: Props): JSX.Element => {
  return (
    <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
      <Text>Signin page</Text>
      <Button title="Signup" onPress={() => navigation.navigate('Signup')} />
      <Button title="Home" onPress={() => navigation.navigate('Home')} />
    </View>
  );
};

export default SigninScreen;
