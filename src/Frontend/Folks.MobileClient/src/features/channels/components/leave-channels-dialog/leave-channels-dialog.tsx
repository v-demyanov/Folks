import {
  Avatar,
  Button,
  Modal,
  Portal,
  Text,
  useTheme,
} from 'react-native-paper';
import { useMemo } from 'react';
import { View } from 'react-native';

import { ILeaveChannelsDialogProps } from '../../models';
import { Theme } from '../../../../themes/types/theme';
import buildStyles from './leave-channels-dialog.styles';

const LeaveChannelsDialog = ({
  visible,
  onDismiss,
  onConfirmPress,
  channels,
}: ILeaveChannelsDialogProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const selectedChannelsCount = useMemo<number>(
    () =>
      channels.reduce((count, channel) => {
        if (channel.isSelected) {
          count++;
        }
        return count;
      }, 0),
    [channels]
  );

  return (
    <Portal>
      <Modal visible={visible} onDismiss={onDismiss} dismissable={false}>
        <View style={[styles.wrapperView]}>
          <View style={[styles.headerView]}>
            {selectedChannelsCount > 1 ? (
              <Text variant="titleMedium">
                Leave {selectedChannelsCount} channels
              </Text>
            ) : (
              <>
                <Avatar.Icon
                  icon="account"
                  size={50}
                  style={[styles.groupImage]}
                />
                <Text variant="titleMedium">
                  {channels.find((x) => x.isSelected)?.title}
                </Text>
              </>
            )}
          </View>
          <View>
            <Text variant="bodyMedium" style={[styles.contentText]}>
              {selectedChannelsCount > 1
                ? 'Are you sure you want to delete and leave these channels?'
                : 'Are you sure you want to delete and leave the channel?'}
            </Text>
          </View>
          <View style={[styles.actionsView]}>
            <Button onPress={onDismiss}>Cancel</Button>
            <Button onPress={onConfirmPress} textColor={theme.colors.error}>
              Leave
            </Button>
          </View>
        </View>
      </Modal>
    </Portal>
  );
};

export default LeaveChannelsDialog;
