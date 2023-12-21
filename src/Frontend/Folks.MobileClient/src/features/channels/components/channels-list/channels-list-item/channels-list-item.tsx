import { Avatar, Badge, List, Text } from 'react-native-paper';
import { View } from 'react-native';

import { IconsConstants } from '../../../../../common';
import buildStyles from './channels-list-item.styles';
import { getUserFrendlyDateString } from '../../../../../common/helpers';
import { IChannelsListItemProps } from '../../../models';

const ChannelsListItem = ({
  channel,
  onPress,
}: IChannelsListItemProps): JSX.Element => {
  const styles = buildStyles();

  return (
    <List.Item
      style={[styles.listItem]}
      title={channel.title}
      titleStyle={[styles.title]}
      description={'Last message...'}
      onPress={() => onPress(channel)}
      left={() => (
        <Avatar.Icon
          size={IconsConstants.LIST_ITEM_IMAGE_SIZE}
          icon="account"
        />
      )}
      right={() => (
        <View style={[styles.view]}>
          <Text variant="bodySmall">
            {getUserFrendlyDateString(new Date())}
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
