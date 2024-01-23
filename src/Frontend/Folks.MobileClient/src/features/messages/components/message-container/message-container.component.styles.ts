import { StyleSheet } from 'react-native';

import { Theme } from '../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    wrapperLeft: {
      maxWidth: '80%',
      display: 'flex',
      flexDirection: 'row',
      alignItems: 'flex-end',
    },
    wrapperRight: {
      maxWidth: '80%',
      display: 'flex',
      flexDirection: 'row-reverse',
      alignItems: 'flex-end',
    },
    contentWrapperLeft: {
      backgroundColor: theme.colors.messageContainer,
      borderTopLeftRadius: 20,
      borderTopRightRadius: 20,
      borderBottomRightRadius: 20,
      padding: 8,
    },
    contentWrapperRight: {
      backgroundColor: theme.colors.ownMessageContainer,
      borderTopLeftRadius: 20,
      borderTopRightRadius: 20,
      borderBottomLeftRadius: 20,
      paddingHorizontal: 8,
      paddingVertical: 8,
    },
    svg: {
      transform: [{ scaleX: -1 }],
      borderRadius: 30,
    },
  });

export default buildStyles;
