import { StyleSheet } from 'react-native';

const buildStyles = () =>
  StyleSheet.create({
    centeredView: {
      height: '100%',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
    },
    errorText: {
      textAlign: 'center',
    },
  });

export default buildStyles;
