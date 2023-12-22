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
import { BlurView } from 'expo-blur';

import { IDeleteChannelDialogProps } from '../../models';
import { Theme } from '../../../../themes/types/theme';
import buildStyles from './delete-channel-dialog.styles';

const DeleteChannelDialog = ({
  visible,
  onDismiss,
  onConfirmPress,
  channels,
}: IDeleteChannelDialogProps): JSX.Element => {
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
        <BlurView style={[styles.blurView]} intensity={70}>
          <View style={[styles.headerView]}>
            {selectedChannelsCount > 1 ? (
              <Text variant="titleMedium">
                Delete {selectedChannelsCount} channels
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
              Delete
            </Button>
          </View>
        </BlurView>
      </Modal>
    </Portal>
  );
};

export default DeleteChannelDialog;
