import { StyleSheet } from 'react-native';

import { Theme } from '../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    wrapperView: {
      backgroundColor: theme.colors.background,
      borderRadius: 10,
      width: '70%',
      padding: 20,
      alignSelf: 'center',
    },
    headerView: {
      display: 'flex',
      flexDirection: 'row',
      alignItems: 'center',
      paddingBottom: 10,
    },
    groupImage: {
      marginRight: 8,
    },
    contentText: {
      paddingBottom: 5,
    },
    actionsView: {
      display: 'flex',
      flexDirection: 'row',
      justifyContent: 'flex-end',
    },
  });

export default buildStyles;
