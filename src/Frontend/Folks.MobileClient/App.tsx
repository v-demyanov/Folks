import { StatusBar } from 'expo-status-bar';
import { PaperProvider } from 'react-native-paper';
import { NavigationContainer } from '@react-navigation/native';
import { useColorScheme } from 'react-native';
import { Provider } from 'react-redux';

import { darkTheme, lightTheme } from './src/common/themes/themes';
import AppNavigator from './src/navigation/app-navigator';
import { AuthProvider } from './src/features/auth/context/auth-provider';
import { store } from './src/common/store/store';

export default function App() {
  const colorScheme = useColorScheme();

  const theme = colorScheme === 'dark' ? darkTheme : lightTheme;

  return (
    <NavigationContainer>
      <PaperProvider theme={theme}>
        <AuthProvider>
          <Provider store={store}>
            <AppNavigator />
            <StatusBar style="auto" />
          </Provider>
        </AuthProvider>
      </PaperProvider>
    </NavigationContainer>
  );
}
