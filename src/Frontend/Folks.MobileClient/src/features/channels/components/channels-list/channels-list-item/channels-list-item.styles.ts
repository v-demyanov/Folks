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
