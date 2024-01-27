import { StyleSheet } from 'react-native';

const buildStyles = () =>
  StyleSheet.create({
    listItem: {
      paddingLeft: 10,
    },
    rightView: {
      display: 'flex',
      flexDirection: 'column',
      justifyContent: 'space-between',
    },
    dateMessageReadIndicatorView: {
      flexDirection: 'row',
    },
    messageReadIndicatorIcon: {
      fontSize: 15,
      paddingRight: 5,
    },
    title: {
      fontWeight: 'bold',
    },
    badge: {
      fontWeight: 'bold',
    },
    descriptionWrapper: {
      flexDirection: 'row',
    },
    messageOwner: {
      color: '#3db5ff',
      fontWeight: 'bold',
    },
    messageTextContent: {
      color: 'gray',
      paddingLeft: 3,
    },
    messageEventContent: {
      color: '#3db5ff',
      fontWeight: 'bold',
    },
  });

export default buildStyles;
