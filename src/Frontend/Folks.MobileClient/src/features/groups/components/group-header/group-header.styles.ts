import { StyleSheet } from 'react-native';

import { Theme } from '../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    header: {
      backgroundColor: theme.colors.primary,
    },
    contentView: {
      display: 'flex',
      flexDirection: 'row',
      alignItems: 'center',
    },
    groupImage: {
      marginRight: 6,
    },
    groupDetailsView: {
      display: 'flex',
      flexDirection: 'column',
    },
    groupTitle: {
      color: theme.colors.onPrimary,
    },
    members: {
      color: theme.colors.onPrimary,
    },
  });

export default buildStyles;
