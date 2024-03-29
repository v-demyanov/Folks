import { useNavigation } from '@react-navigation/native';
import { useMemo, useRef, useState } from 'react';
import { Vibration } from 'react-native';

import { InteractionConstants } from '../../../common';
import { InformationContainer } from '../../../common/components';
import { useArrayEffect } from '../../../common/hooks';
import { SelectableItem } from '../../../common/models';
import {
  useGetOwnChannelsQuery,
  useLeaveChannelsMutation,
} from '../../../features/channels';
import {
  CreateChannelButton,
  ChannelsList,
  ChannelsToolbar,
  LeaveChannelsDialog,
} from '../../../features/channels/components';
import {
  IChannel,
  ILeaveChannelRequest,
} from '../../../features/channels/models';
import HomeAppbar from '../../../features/home/home-appbar/home-appbar';
import { StackNavigation } from '../../../navigation/app-navigator';

const HomeScreen = (): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();

  const { data: channels = [], isError: isGetChannelsError } =
    useGetOwnChannelsQuery(null);
  const [leaveChannels, { isLoading: isLeavingChannels }] =
    useLeaveChannelsMutation();

  const [selectableChannels, setSelectableChannels] = useState<
    SelectableItem<IChannel>[]
  >([]);
  const [wasPressTimerRefCalled, setWasPressTimerRefCalled] = useState(false);
  const pressTimerRef = useRef<NodeJS.Timeout>();

  const selectedChannelsCount = useMemo(
    (): number =>
      selectableChannels.reduce((count, item) => {
        if (item.isSelected) {
          count++;
        }
        return count;
      }, 0),
    [selectableChannels],
  );
  const isSelectableMode = useMemo(
    (): boolean => selectedChannelsCount > 0,
    [selectedChannelsCount],
  );

  useArrayEffect(() => {
    const selectableChannels = prepareSelectableChannels();
    setSelectableChannels(selectableChannels);
  }, [channels]);

  function prepareSelectableChannels(): SelectableItem<IChannel>[] {
    return channels
      .map((channel) => ({
        ...channel,
        isSelected: false,
      }))
      .sort((x, y) => {
        const dateX = x.lastMessage
          ? new Date(x.lastMessage.sentAt)
          : new Date(x.createdAt);
        const dateY = y.lastMessage
          ? new Date(y.lastMessage.sentAt)
          : new Date(y.createdAt);

        return dateY.getTime() - dateX.getTime();
      });
  }

  function getChannelsErrorMessage(): string {
    return 'Oops! Something went wrong,\n while channels loading.';
  }

  function handleListItemPress(channel: SelectableItem<IChannel>): void {
    if (wasPressTimerRefCalled) {
      setWasPressTimerRefCalled(false);
      return;
    }

    if (isSelectableMode) {
      channel.isSelected = !channel.isSelected;
      setSelectableChannels([...selectableChannels]);
    } else {
      navigation.navigate('Group', channel);
    }
  }

  function handleListItemPressIn(channel: SelectableItem<IChannel>): void {
    pressTimerRef.current = setTimeout(() => {
      channel.isSelected = !channel.isSelected;

      setWasPressTimerRefCalled(true);
      setSelectableChannels([...selectableChannels]);

      Vibration.vibrate(
        InteractionConstants.LONGPRESS_CALLBACK_VIBRATION_PATTERN,
      );
    }, InteractionConstants.LONGPRESS_CALLBACK_MS);
  }

  function handleListItemPressOut(): void {
    clearTimeout(pressTimerRef.current);
  }

  function handleCancelPress(): void {
    selectableChannels.forEach((channel) => (channel.isSelected = false));
    setSelectableChannels([...selectableChannels]);
  }

  const [leaveChannelsDialogVisible, setLeaveChannelsDialogVisible] =
    useState(false);

  function handleLeavePress(): void {
    setLeaveChannelsDialogVisible(true);
  }

  function handleLeaveChannelsDialogDissmiss(): void {
    setLeaveChannelsDialogVisible(false);
  }

  async function handleLeaveChannelsDialogConfirm(): Promise<void> {
    const requests = selectableChannels
      .filter((channel) => channel.isSelected)
      .map(
        (channel) =>
          ({
            channelId: channel.id,
            channelType: channel.type,
          }) as ILeaveChannelRequest,
      );

    await leaveChannels(requests).unwrap();
    setLeaveChannelsDialogVisible(false);
  }

  return (
    <>
      {isSelectableMode ? (
        <ChannelsToolbar
          selectedChannelsCount={selectedChannelsCount}
          onCancelPress={handleCancelPress}
          onLeavePress={handleLeavePress}
        />
      ) : (
        <HomeAppbar />
      )}
      {isGetChannelsError ? (
        <InformationContainer message={getChannelsErrorMessage()} />
      ) : (
        <ChannelsList
          channels={selectableChannels}
          onListItemPress={handleListItemPress}
          onListItemPressIn={handleListItemPressIn}
          onListItemPressOut={handleListItemPressOut}
        />
      )}
      <CreateChannelButton />
      <LeaveChannelsDialog
        visible={leaveChannelsDialogVisible}
        onDismiss={handleLeaveChannelsDialogDissmiss}
        onConfirmPress={handleLeaveChannelsDialogConfirm}
        channels={selectableChannels}
        cancelButtonDisabled={isLeavingChannels}
        leaveButtonDisabled={isLeavingChannels}
      />
    </>
  );
};

export default HomeScreen;
