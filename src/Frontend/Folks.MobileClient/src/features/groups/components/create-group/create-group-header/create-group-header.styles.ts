import { StyleSheet } from 'react-native';

import { Theme } from '../../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    header: {
      backgroundColor: theme.colors.primary,
    },
    content: {
      fontWeight: 'bold',
      color: theme.colors.onPrimary,
    },
  });

export default buildStyles;
