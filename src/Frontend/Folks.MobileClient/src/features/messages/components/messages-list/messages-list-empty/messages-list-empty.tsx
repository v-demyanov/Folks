import { View } from 'react-native';
import { Text } from 'react-native-paper';

import buildStyles from './messages-list-empty.styles';

const MessagesListEmpty = (): JSX.Element => {
  const styles = buildStyles();

  return (
    <View style={[styles.centeredView]}>
      <Text variant="labelMedium">
        Congratulations, you have created a group!
      </Text>
    </View>
  );
};

export default MessagesListEmpty;
