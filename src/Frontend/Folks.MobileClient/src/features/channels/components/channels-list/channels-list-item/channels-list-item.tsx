import { Avatar, Badge, List, Text } from 'react-native-paper';
import { View } from 'react-native';
import { useState } from 'react';

import { IconsConstants } from '../../../../../common';
import buildStyles from './channels-list-item.styles';
import { getUserFrendlyDateString } from '../../../../../common/helpers';
import { IChannelsListItemProps } from '../../../models';
import { ListCheckBox } from '../../../../../common/components';
import { useGetMessagesQuery } from '../../../../messages';
import { IGetMessagesQuery } from '../../../../messages/models';

const ChannelsListItem = ({
  channel,
  onPress,
  onPressIn,
  onPressOut,
}: IChannelsListItemProps): JSX.Element => {
  const styles = buildStyles();
  const [isOnFocus, setIsOnFocus] = useState(false);

  const { lastMessage } = useGetMessagesQuery(
    {
      channelId: channel.id,
      channelType: channel.type,
    } as IGetMessagesQuery,
    {
      selectFromResult: ({ data }) => ({
        lastMessage: data?.slice(-1).pop(),
      }),
    }
  );

  function handlePressIn(): void {
    setIsOnFocus(true);
    if (onPressIn) {
      onPressIn(channel);
    }
  }

  function handlePressOut(): void {
    setIsOnFocus(false);
    if (onPressOut) {
      onPressOut(channel);
    }
  }

  function handlePress(): void {
    if (onPress) {
      onPress(channel);
    }
  }

  return (
    <List.Item
      style={[styles.listItem]}
      title={channel.title}
      titleStyle={[styles.title]}
      description={lastMessage?.content}
      onPress={handlePress}
      onPressIn={handlePressIn}
      onPressOut={handlePressOut}
      left={() => (
        <>
          <Avatar.Icon
            size={IconsConstants.LIST_ITEM_IMAGE_SIZE}
            icon="account"
          />
          {channel.isSelected ? <ListCheckBox isOnFocus={isOnFocus} /> : null}
        </>
      )}
      right={() => (
        <View style={[styles.view]}>
          <Text variant="bodySmall">
            {getUserFrendlyDateString(
              lastMessage ? new Date(lastMessage.sentAt) : new Date(),
              { formatInHHMM: true }
            )}
          </Text>
          <Badge style={[styles.badge]} size={22}>
            3
          </Badge>
        </View>
      )}
    />
  );
};

export default ChannelsListItem;
