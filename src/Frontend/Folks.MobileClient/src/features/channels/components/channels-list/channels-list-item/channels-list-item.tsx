import { Avatar, Badge, List, Text } from 'react-native-paper';
import { View } from 'react-native';
import { useState } from 'react';
import Icon from 'react-native-vector-icons/MaterialIcons';

import { IconsConstants } from '../../../../../common';
import buildStyles from './channels-list-item.styles';
import { getUserFrendlyDateString } from '../../../../../common/helpers';
import { IChannelsListItemProps } from '../../../models';
import { ListCheckBox } from '../../../../../common/components';
import { MessageType } from '../../../../messages/enums';
import { useAuth } from '../../../../auth/hooks';
import { formatMessageContentByType } from '../../../../messages/helpers';

const ChannelsListItem = ({
  channel,
  onPress,
  onPressIn,
  onPressOut,
}: IChannelsListItemProps): JSX.Element => {
  const styles = buildStyles();
  const [isOnFocus, setIsOnFocus] = useState(false);

  const { currentUser } = useAuth();

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

  function renderDescription(): JSX.Element {
    switch (channel.lastMessage?.type) {
      case MessageType.Text:
        return (
          <View style={[styles.descriptionWrapper]}>
            <View>
              <Text style={[styles.messageOwner]}>
                {currentUser?.sub === channel.lastMessage.owner.id
                  ? 'You'
                  : channel.lastMessage.owner.userName}
                :
              </Text>
            </View>
            <View>
              <Text style={[styles.messageTextContent]}>
                {channel.lastMessage.content}
              </Text>
            </View>
          </View>
        );
      case MessageType.NewGroupOwnerSetEvent:
      case MessageType.UserLeftEvent:
        return (
          <Text style={[styles.messageEventContent]}>
            {formatMessageContentByType(channel.lastMessage)}
          </Text>
        );
      default:
        return <></>;
    }
  }

  function getMessageReadIndicatorName(): string {
    if (channel.lastMessage && channel.lastMessage.readBy.length > 1) {
      return 'done-all';
    }

    return 'check';
  }

  return (
    <List.Item
      style={[styles.listItem]}
      title={channel.title}
      titleStyle={[styles.title]}
      description={renderDescription()}
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
        <View style={[styles.rightView]}>
          <View style={[styles.dateMessageReadIndicatorView]}>
            {channel.lastMessage?.owner.id === currentUser?.sub ? (
              <Icon
                name={getMessageReadIndicatorName()}
                style={[styles.messageReadIndicatorIcon]}
              />
            ) : null}
            <Text variant="bodySmall">
              {getUserFrendlyDateString(
                channel.lastMessage
                  ? new Date(channel.lastMessage.sentAt)
                  : new Date(channel.createdAt),
                { formatInHHMM: true }
              )}
            </Text>
          </View>

          {channel.unreadMessagesCount > 0 ? (
            <Badge style={[styles.badge]} size={22}>
              {channel.unreadMessagesCount}
            </Badge>
          ) : null}
        </View>
      )}
    />
  );
};

export default ChannelsListItem;
