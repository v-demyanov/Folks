import { useMemo } from 'react';
import { View } from 'react-native';
import { Appbar, IconButton, Text, useTheme } from 'react-native-paper';

import buildStyles from './channels-toolbar.styles';
import { Theme } from '../../../../themes/types/theme';
import IChannelsToolbarProps from '../../models/channels-toolbar.props';

const ChannelsToolbar = ({
  selectedChannelsCount,
  onCancelPress,
  onLeavePress,
}: IChannelsToolbarProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <Appbar.Header style={[styles.header]}>
      <Appbar.Content
        title={
          <View style={[styles.content]}>
            <View style={[styles.selectedCountView]}>
              <IconButton
                icon="close"
                iconColor={theme.colors.onPrimary}
                onPress={onCancelPress}
              />
              <Text style={styles.selectedCount} variant="titleMedium">
                {selectedChannelsCount}
              </Text>
            </View>
            <View>
              <IconButton
                icon="trash-can-outline"
                iconColor={theme.colors.onPrimary}
                onPress={onLeavePress}
              />
            </View>
          </View>
        }
      />
    </Appbar.Header>
  );
};

export default ChannelsToolbar;
