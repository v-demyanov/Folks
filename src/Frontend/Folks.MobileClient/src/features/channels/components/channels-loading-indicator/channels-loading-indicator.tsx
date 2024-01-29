import { useMemo } from 'react';
import { View } from 'react-native';
import { ActivityIndicator, Text, useTheme } from 'react-native-paper';

import buildStyles from './channels-loading-indicator.styles';
import { Theme } from '../../../../themes/types/theme';
import { useGetOwnChannelsQuery } from '../../api/channels.api';

const ChannelsLoadingIndicator = (): JSX.Element | null => {
  const { isLoading } = useGetOwnChannelsQuery(null);

  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

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
