import { StyleSheet } from 'react-native';

import { Theme } from '../../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    wrapper: {
      backgroundColor: theme.colors.background,
      display: 'flex',
      flexDirection: 'row',
      paddingHorizontal: 20,
      paddingVertical: 10,
    },
    groupImageView: {
      width: '30%',
      display: 'flex',
      justifyContent: 'center',
    },
    inputView: {
      width: '70%',
      display: 'flex',
    },
    textInput: {
      backgroundColor: theme.colors.background,
      width: '100%',
    },
  });

export default buildStyles;
