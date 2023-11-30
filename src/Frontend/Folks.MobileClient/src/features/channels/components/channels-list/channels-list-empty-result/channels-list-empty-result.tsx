import { View } from 'react-native';
import { Text } from 'react-native-paper';

import buildStyles from './channels-list-empty-result.styles';

const ChannelsListEmptyResult = (): JSX.Element => {
  const styles = buildStyles();

  return (
    <View style={[styles.centeredView]}>
      <Text variant="labelMedium">Create your first group or chat!</Text>
    </View>
  );
};

export default ChannelsListEmptyResult;