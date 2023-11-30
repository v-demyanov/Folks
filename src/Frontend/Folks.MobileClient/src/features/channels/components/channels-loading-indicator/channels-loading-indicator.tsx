import { View } from 'react-native';
import { ActivityIndicator, Text } from 'react-native-paper';

import { useGetOwnChannelsQuery } from '../../api/channels.api';
import buildStyles from './channels-loading-indicator.styles';

const ChannelsLoadingIndicator = (): JSX.Element | null => {
  const { isLoading } = useGetOwnChannelsQuery(null);

  const styles = buildStyles();

  if (!isLoading) {
    return null;
  }

  return (
    <View style={[styles.connectingIndicatorView]}>
      <ActivityIndicator />
      <Text variant="titleMedium" style={[styles.connectingIndicatorText]}>
        Connecting...
      </Text>
    </View>
  );
};

export default ChannelsLoadingIndicator;
