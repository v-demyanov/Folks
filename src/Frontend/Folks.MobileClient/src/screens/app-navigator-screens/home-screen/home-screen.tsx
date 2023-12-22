import { useRef, useState } from 'react';
import { useNavigation } from '@react-navigation/native';
import { Vibration } from 'react-native';

import { useGetOwnChannelsQuery } from '../../../features/channels';
import {
  CreateChannelButton,
  ChannelsList,
} from '../../../features/channels/components';
import HomeAppbar from '../../../features/home/home-appbar/home-appbar';
import { IChannel } from '../../../features/channels/models';
import { SelectableItem } from '../../../common/models';
import { useArrayEffect } from '../../../common/hooks';
import { InformationContainer } from '../../../common/components';
import { StackNavigation } from '../../../navigation/app-navigator';
import { InteractionConstants } from '../../../common';

const HomeScreen = (): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();

  const { data: channels = [], isError: isGetChannelsError } =
    useGetOwnChannelsQuery(null);

  const [selectableChannels, setSelectableChannels] = useState<
    SelectableItem<IChannel>[]
  >([]);
  const [wasPressTimerRefCalled, setWasPressTimerRefCalled] = useState(false);
  const pressTimerRef = useRef<NodeJS.Timeout>();

  useArrayEffect(() => {
    const selectableChannels = prepareSelectableChannels();
    setSelectableChannels(selectableChannels);
  }, [channels]);

  function prepareSelectableChannels(): SelectableItem<IChannel>[] {
    return channels.map((channel) => ({
      ...channel,
      isSelected: false,
    }));
  }

  function getChannelsErrorMessage(): string {
    return 'Oops! Something went wrong,\n while channels loading.';
  }

  function handleListItemPress(channel: SelectableItem<IChannel>): void {
    if (wasPressTimerRefCalled) {
      setWasPressTimerRefCalled(false);
      return;
    }

    if (isSelectableMode()) {
      channel.isSelected = !channel.isSelected;
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
        InteractionConstants.LONGPRESS_CALLBACK_VIBRATION_PATTERN
      );
    }, InteractionConstants.LONGPRESS_CALLBACK_MS);
  }

  function handleListItemPressOut(): void {
    clearTimeout(pressTimerRef.current);
  }

  function isSelectableMode(): boolean {
    return selectableChannels.some((channel) => channel.isSelected);
  }

  return (
    <>
      <HomeAppbar />
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
    </>
  );
};

export default HomeScreen;
