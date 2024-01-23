import { StyleSheet } from 'react-native';

import { Theme } from '../../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    wrapperLeft: {
      width: '100%',
      alignItems: 'flex-start',
      marginBottom: 5,
      paddingLeft: 10,
    },
    wrapperRight: {
      width: '100%',
      alignItems: 'flex-end',
      marginBottom: 5,
      paddingRight: 10,
    },
    wrapperLevel2: {
      flexDirection: 'row',
      alignItems: 'flex-end',
    },
    userNameText: {
      color: theme.colors.messageContainerTitle,
    },
    contentText: {
      color: theme.colors.messageContainerContent,
    },
    avatar: {
      marginRight: 5,
    },
    messageSentAtText: {
      marginLeft: 50,
      color: theme.colors.onSecondaryContainer,
    },
    specificMessageView: {
      backgroundColor: theme.colors.dateContainer,
      paddingHorizontal: 5,
      paddingVertical: 2,
      borderRadius: 40,
      overflow: 'hidden',
      alignSelf: 'center',
      marginVertical: 5,
    },
  });

export default buildStyles;
