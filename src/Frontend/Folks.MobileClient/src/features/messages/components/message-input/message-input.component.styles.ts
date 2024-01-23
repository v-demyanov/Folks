import { StyleSheet } from 'react-native';

import { Theme } from '../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    wrapper: {
      backgroundColor: theme.colors.background,
      display: 'flex',
      flexDirection: 'row',
      justifyContent: 'space-between',
    },
    textInput: {
      backgroundColor: theme.colors.background,
      flex: 1,
      marginLeft: 10,
      fontSize: 16,
    },
  });

export default buildStyles;
