import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationProp } from '@react-navigation/native';

import ChatScreen from '../screens/app-navigator-screens/chat-screen/chat-screen';
import HomeScreen from '../screens/app-navigator-screens/home-screen/home-screen';
import WelcomeScreen from '../screens/app-navigator-screens/welcome-screen/welcome-screen';
import useAuth from '../features/auth/hooks/use-auth';
import CreateGroupScreen from '../screens/app-navigator-screens/create-group-screen/create-group-screen';
import GroupScreen from '../screens/app-navigator-screens/group-screen/group-screen';
import IChannel from '../features/channels/models/channel';

export type RootStackParamList = {
  Home: undefined;
  Chat: undefined;
  Welcome: undefined;
  CreateGroup: undefined;
  Group: IChannel;
};

export type StackNavigation = NavigationProp<RootStackParamList>;

const Stack = createNativeStackNavigator<RootStackParamList>();

const AppNavigator = (): JSX.Element => {
  const { isAuthenticated } = useAuth();

  return (
    <Stack.Navigator
      initialRouteName="Welcome"
      screenOptions={{ headerShown: false }}
    >
      {isAuthenticated() ? (
        <>
          <Stack.Screen name="Home" component={HomeScreen} />
          <Stack.Screen name="Chat" component={ChatScreen} />
          <Stack.Screen name="CreateGroup" component={CreateGroupScreen} />
          <Stack.Screen name="Group" component={GroupScreen} />
        </>
      ) : (
        <Stack.Screen name="Welcome" component={WelcomeScreen} />
      )}
    </Stack.Navigator>
  );
};

export default AppNavigator;
