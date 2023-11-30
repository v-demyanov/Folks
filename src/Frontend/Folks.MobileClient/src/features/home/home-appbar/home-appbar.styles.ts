import { StyleSheet } from 'react-native';

const buildStyles = () =>
  StyleSheet.create({
    titleView: {
      display: 'flex',
      flexDirection: 'row',
      alignItems: 'center',
      justifyContent: 'space-between',
    },
  });

export default buildStyles;
