import { StyleSheet } from 'react-native';

import { Theme } from '../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    flatList: {
      backgroundColor: theme.colors.background
    },
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
