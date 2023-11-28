import { StyleSheet } from 'react-native';

import { Theme } from '../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    chip: {
      marginLeft: 5,
      marginRight: 5,
    },
    scrollView: {
      paddingVertical: 10,
      paddingHorizontal: 5,
      backgroundColor: theme.colors.background,
    },
    emptyView: {
      paddingVertical: 4,
      paddingLeft: 5,
    },
  });

export default buildStyles;
