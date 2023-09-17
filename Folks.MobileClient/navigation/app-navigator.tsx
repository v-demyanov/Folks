import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationProp } from '@react-navigation/native';

import SigninScreen from '../screens/access/signin-screen/signin-screen';
import SignupScreen from '../screens/access/signup-screen/signup-screen';
import ChatScreen from '../screens/chat-screen/chat-screen';
import HomeScreen from '../screens/home/home-screen';

export type RootStackParamList = {
  Home: undefined;
  Chat: undefined;
  Signin: undefined;
  Signup: undefined;
};

export type StackNavigation = NavigationProp<RootStackParamList>;

const Stack = createNativeStackNavigator<RootStackParamList>();

const AppNavigator = (): JSX.Element => {
  return (
    <Stack.Navigator
      initialRouteName="Signin"
      screenOptions={{ headerShown: false }}
    >
      <Stack.Screen name="Home" component={HomeScreen} />
      <Stack.Screen name="Chat" component={ChatScreen} />
      <Stack.Screen name="Signin" component={SigninScreen} />
      <Stack.Screen name="Signup" component={SignupScreen} />
    </Stack.Navigator>
  );
};

export default AppNavigator;
