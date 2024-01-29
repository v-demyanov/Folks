import { NavigationContainer } from '@react-navigation/native';
import { StatusBar } from 'expo-status-bar';
import { useColorScheme } from 'react-native';
import { PaperProvider } from 'react-native-paper';
import { Provider } from 'react-redux';
import 'expo-dev-client';

import './src/common/extensions/array.extensions';
import { AuthProvider } from './src/features/auth/context/auth-provider';
import AppNavigator from './src/navigation/app-navigator';
import { store } from './src/store/store';
import { darkTheme, lightTheme } from './src/themes/themes';

export default function App() {
  const colorSchemeName = useColorScheme();
  const theme = colorSchemeName?.toString() === 'dark' ? darkTheme : lightTheme;

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
