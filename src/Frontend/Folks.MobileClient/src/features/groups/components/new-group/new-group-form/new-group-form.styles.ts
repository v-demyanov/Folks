import { StyleSheet } from 'react-native';

import { Theme } from '../../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    wrapper: {
      backgroundColor: theme.colors.background,
      display: 'flex',
      flexDirection: 'row',
      paddingHorizontal: 20,
    },
    groupImageView: {
      width: '25%',
      display: 'flex',
      justifyContent: 'center',
    },
    inputView: {
      width: '75%',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
    },
    textInput: {
      backgroundColor: theme.colors.background,
      width: '100%',
    },
  });

export default buildStyles;
