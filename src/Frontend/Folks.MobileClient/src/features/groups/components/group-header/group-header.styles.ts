import { StyleSheet } from 'react-native';

const buildStyles = () =>
  StyleSheet.create({
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
  });

export default buildStyles;
