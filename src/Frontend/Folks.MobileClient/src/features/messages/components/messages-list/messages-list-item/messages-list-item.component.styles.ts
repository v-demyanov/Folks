import { StyleSheet } from 'react-native';

import { Theme } from '../../../../../themes/types/theme';

const buildStyles = (theme: Theme) =>
  StyleSheet.create({
    wrapper: {
      width: '100%',
      marginBottom: 5,
      paddingLeft: 10,
    },
    wrapperLeft: {
      width: '100%',
      marginBottom: 5,
      paddingLeft: 10,
      flexDirection: 'row',
      alignItems: 'flex-end',
    },
    wrapperRight: {
      width: '100%',
      marginBottom: 5,
      paddingRight: 10,
      flexDirection: 'row-reverse',
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
    messageInfoView: {
      flexDirection: 'row',
    },
    messageInfoViewLeft: {
      alignSelf: 'flex-start',
    },
    messageInfoViewRight: {
      alignSelf: 'flex-end',
    },
    messageSentAtText: {
      color: theme.colors.messageContainerContent,
    },
    messageCheckIcon: {
      color: theme.colors.messageContainerContent,
      paddingLeft: 5,
      fontSize: 15,
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
