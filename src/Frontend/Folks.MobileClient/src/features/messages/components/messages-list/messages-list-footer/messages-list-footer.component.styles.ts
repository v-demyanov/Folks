import { StyleSheet } from 'react-native';

import { Theme } from '../../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    view: {
      backgroundColor: theme.colors.dateContainer,
      paddingHorizontal: 5,
      paddingVertical: 2,
      borderRadius: 40,
      overflow: 'hidden',
      alignSelf: 'center',
      marginVertical: 5,
    },
    text: {
      color: theme.colors.messageContainerContent,
    },
  });

export default buildStyles;
