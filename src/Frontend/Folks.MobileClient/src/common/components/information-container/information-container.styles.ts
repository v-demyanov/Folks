import { StyleSheet } from 'react-native';

import { Theme } from '../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    view: {
      flex: 1,
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      backgroundColor: theme.colors.background,
    },
    text: {
      textAlign: 'center',
    },
  });

export default buildStyles;
