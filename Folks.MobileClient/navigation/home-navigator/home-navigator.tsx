import MaterialCommunityIcons from 'react-native-vector-icons/MaterialCommunityIcons';
import { createMaterialBottomTabNavigator } from 'react-native-paper/react-navigation';
import { useTheme } from 'react-native-paper';

import UsersScreen from '../../screens/home/chats-screen/chats-screen';
import SettingsScreen from '../../screens/home/settings-screen/settings-screen';
import { TAB_BAR_ICON_SIZE } from '../../common/constants/icons.constants';

export type TabParamList = {
  Chats: undefined;
  Settings: undefined;
};

const Tab = createMaterialBottomTabNavigator<TabParamList>();

const HomeNavigator = (): JSX.Element => {
  const theme = useTheme();

  return (
    <Tab.Navigator activeColor={theme.colors.primary}>
      <Tab.Screen
        name="Chats"
        component={UsersScreen}
        options={{
          tabBarIcon: ({ color }) => (
            <MaterialCommunityIcons
              name="chat"
              color={color}
              size={TAB_BAR_ICON_SIZE}
            />
          ),
        }}
      />
      <Tab.Screen
        name="Settings"
        component={SettingsScreen}
        options={{
          tabBarIcon: ({ color }) => (
            <MaterialCommunityIcons
              name="cog"
              color={color}
              size={TAB_BAR_ICON_SIZE}
            />
          ),
        }}
      />
    </Tab.Navigator>
  );
};

export default HomeNavigator;
