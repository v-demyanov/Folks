import { View } from 'react-native';
import { Text } from 'react-native-paper';

import buildStyles from './channels-list-error.styles';

const ChannelsListError = (): JSX.Element => {
  const styles = buildStyles();

  return (
    <View style={[styles.centeredView]}>
      <Text variant="labelMedium" style={[styles.errorText]}>
        Oops! Something went wrong,{'\n'} while channels loading...
      </Text>
    </View>
  );
};

export default ChannelsListError;
