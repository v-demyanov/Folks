import { StyleSheet } from 'react-native';

const buildStyles = () =>
  StyleSheet.create({
    listItem: {
      paddingLeft: 10,
    },
    view: {
      display: 'flex',
      flexDirection: 'column',
      justifyContent: 'space-between',
    },
    title: {
      fontWeight: 'bold',
    },
    badge: {
      fontWeight: 'bold',
    },
  });

export default buildStyles;
