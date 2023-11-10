import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationProp } from '@react-navigation/native';

import ChatScreen from '../screens/chat-screen/chat-screen';
import HomeScreen from '../screens/home/home-screen';
import WelcomeScreen from '../screens/access/welcome-screen/welcome-screen';

export type RootStackParamList = {
  Home: undefined;
  Chat: undefined;
  Welcome: undefined;
};

export type StackNavigation = NavigationProp<RootStackParamList>;

const Stack = createNativeStackNavigator<RootStackParamList>();

const AppNavigator = (): JSX.Element => {
  return (
    <Stack.Navigator
      initialRouteName="Welcome"
      screenOptions={{ headerShown: false }}
    >
      <Stack.Screen name="Home" component={HomeScreen} />
      <Stack.Screen name="Chat" component={ChatScreen} />
      <Stack.Screen name="Welcome" component={WelcomeScreen} />
    </Stack.Navigator>
  );
};

export default AppNavigator;
