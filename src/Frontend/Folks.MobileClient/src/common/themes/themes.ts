import { MD3DarkTheme, MD3LightTheme } from 'react-native-paper';

import darkColorScheme from './color-schemes/dark-color-scheme.json';
import lightColorScheme from './color-schemes/light-color-scheme.json';

const darkTheme = {
  ...MD3DarkTheme,
  colors: darkColorScheme.colors,
};

const lightTheme = {
  ...MD3LightTheme,
  colors: lightColorScheme.colors,
};

export { darkTheme, lightTheme };
