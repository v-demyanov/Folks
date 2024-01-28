import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationProp } from '@react-navigation/native';

import {
  HomeScreen,
  WelcomeScreen,
  CreateGroupScreen,
  GroupScreen,
} from '../screens';
import { useAuth } from '../features/auth/hooks';
import { IChannel } from '../features/channels/models';

export type RootStackParamList = {
  Home: undefined;
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
          <Stack.Screen
            name="Home"
            component={HomeScreen}
            options={{ animation: 'slide_from_right' }}
          />
          <Stack.Screen
            name="CreateGroup"
            component={CreateGroupScreen}
            options={{ animation: 'slide_from_right' }}
          />
          <Stack.Screen
            name="Group"
            component={GroupScreen}
            options={{ animation: 'slide_from_right' }}
          />
        </>
      ) : (
        <Stack.Screen
          name="Welcome"
          component={WelcomeScreen}
          options={{ animation: 'slide_from_right' }}
        />
      )}
    </Stack.Navigator>
  );
};

export default AppNavigator;
