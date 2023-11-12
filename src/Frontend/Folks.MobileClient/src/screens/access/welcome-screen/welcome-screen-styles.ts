import { MD3Theme } from 'react-native-paper';
import { StyleSheet } from 'react-native';

const buildStyles = (theme: MD3Theme) =>
  StyleSheet.create({
    wrappper: {
      backgroundColor: theme.colors.background,
      height: '100%',
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center',
    },
    logo: {
      paddingBottom: 10,
    },
    image: {
      width: 300,
      height: 250,
    },
    welcomeMessagesWrapper: {
      display: 'flex',
      justifyContent: 'center',
      paddingTop: 10,
    },
    mainText: {
      textAlign: 'center',
      fontWeight: 'bold',
    },
    secondaryText: {
      textAlign: 'center',
      color: theme.colors.onSurfaceVariant,
      maxWidth: 320,
    },
    actionsWrapper: {
      display: 'flex',
      flexDirection: 'row',
      paddingBottom: 20,
      paddingTop: 40,
      columnGap: 10,
    },
  });

export default buildStyles;
