import MaterialCommunityIcons from 'react-native-vector-icons/MaterialCommunityIcons';
import { createMaterialBottomTabNavigator } from 'react-native-paper/react-navigation';
import { useTheme } from 'react-native-paper';

import SettingsScreen from '../../screens/home/settings-screen/settings-screen';
import { TAB_BAR_ICON_SIZE } from '../../common/constants/icons.constants';
import ChannelsScreen from '../../screens/home/channels-screen/channels-screen';

export type TabParamList = {
  Channels: undefined;
  Settings: undefined;
};

const Tab = createMaterialBottomTabNavigator<TabParamList>();

const HomeNavigator = (): JSX.Element => {
  const theme = useTheme();

  return (
    <Tab.Navigator activeColor={theme.colors.primary}>
      <Tab.Screen
        name="Channels"
        component={ChannelsScreen}
        options={{
          tabBarIcon: ({ color }) => (
            <MaterialCommunityIcons
              name="chat"
              color={color}
              size={TAB_BAR_ICON_SIZE}
            />
          ),
          tabBarBadge: 6
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
