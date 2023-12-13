import { StyleSheet } from 'react-native';

import { Theme } from '../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    stickyFooter: {
      position: 'absolute',
      zIndex: 1,
    },
    safeAreaView: {
      flex: 1,
      backgroundColor: theme.colors.secondaryContainer,
      paddingBottom: 3,
    },
    sectionList: {
      backgroundColor: theme.colors.secondaryContainer,
    },
  });

export default buildStyles;
