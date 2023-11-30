import { StyleSheet } from 'react-native';

import { Theme } from '../../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    usersQueryError: {
      height: '100%',
      paddingTop: '60%',
      display: 'flex',
      alignItems: 'center',
      backgroundColor: theme.colors.background,
    },
    usersQueryErrorText: {
      textAlign: 'center',
    },
  });

export default buildStyles;
