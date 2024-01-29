import { MD3Theme } from 'react-native-paper';

import { Colors } from './colors';

export type Theme = Omit<MD3Theme, 'colors'> & {
  colors: Colors;
};
