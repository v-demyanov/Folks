import { StyleSheet } from 'react-native';
import { MD3Theme } from 'react-native-paper/lib/typescript/types';

const buildStyles = (theme: MD3Theme) =>
  StyleSheet.create({
    itemSeparator: {
      backgroundColor: theme.colors.inverseOnSurface,
      height: 1,
      width: '80%',
      marginLeft: '20%',
    },
    centeredView: {
      height: '100%',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
    },
  });

export default buildStyles;
