import { StyleSheet } from 'react-native';

import { Theme } from '../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    header: {
      backgroundColor: theme.colors.primary,
    },
    titleView: {
      display: 'flex',
      flexDirection: 'row',
      alignItems: 'center',
      justifyContent: 'space-between',
    },
  });

export default buildStyles;
