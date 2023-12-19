import { StatusBar } from 'expo-status-bar';
import { PaperProvider } from 'react-native-paper';
import { NavigationContainer } from '@react-navigation/native';
import { useColorScheme } from 'react-native';
import { Provider } from 'react-redux';

import './src/common/extensions/array.extensions';
import { darkTheme, lightTheme } from './src/themes/themes';
import AppNavigator from './src/navigation/app-navigator';
import { AuthProvider } from './src/features/auth/context/auth-provider';
import { store } from './src/store/store';

export default function App() {
  const colorSchemeName = useColorScheme();
  const theme = colorSchemeName == 'dark' ? darkTheme : lightTheme;

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
