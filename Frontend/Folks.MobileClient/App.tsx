import { StatusBar } from 'expo-status-bar';
import { PaperProvider } from 'react-native-paper';
import { NavigationContainer } from '@react-navigation/native';
import { useColorScheme } from 'react-native';

import AppNavigator from './navigation/app-navigator';
import { darkTheme, lightTheme } from './common/themes/themes';

export default function App() {
  const colorScheme = useColorScheme();

  const theme = colorScheme === 'dark' ? darkTheme : lightTheme;

  return (
    <NavigationContainer>
      <PaperProvider theme={theme}>
        <AppNavigator />
        <StatusBar style="auto" />
      </PaperProvider>
    </NavigationContainer>
  );
}
