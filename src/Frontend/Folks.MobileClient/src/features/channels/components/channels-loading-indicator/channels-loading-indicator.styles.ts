import { StyleSheet } from 'react-native';

const buildStyles = () =>
  StyleSheet.create({
    connectingIndicatorView: {
      display: 'flex',
      flexDirection: 'row',
    },
    connectingIndicatorText: {
      paddingLeft: 2,
    },
  });

export default buildStyles;
