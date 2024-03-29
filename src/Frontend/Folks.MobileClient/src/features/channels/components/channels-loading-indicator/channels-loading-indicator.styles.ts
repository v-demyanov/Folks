import { StyleSheet } from 'react-native';

import { Theme } from '../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    connectingIndicatorView: {
      display: 'flex',
      flexDirection: 'row',
    },
    connectingIndicatorText: {
      paddingLeft: 2,
      color: theme.colors.onPrimary,
    },
  });

export default buildStyles;
