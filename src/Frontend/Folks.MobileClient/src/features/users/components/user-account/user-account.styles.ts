import { StyleSheet } from 'react-native';

import { Theme } from '../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    view: {
      display: 'flex',
      flexDirection: 'row',
      alignItems: 'center',
    },
    avatarIcon: {
      marginRight: 6,
    },
    userName: {
      color: theme.colors.onPrimary,
    },
  });

export default buildStyles;
