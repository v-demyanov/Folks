import { Avatar, Badge, List, Text } from 'react-native-paper';
import { View } from 'react-native';

import { LIST_ITEM_IMAGE_SIZE } from '../../../../../common/constants/icons.constants';
import buildStyles from './channels-list-item.styles';
import IChannel from '../../../models/channel';
import { getUserFrendlyDateString } from '../../../../../common/helpers/date-helpers';

const ChannelsListItem = ({ channel }: { channel: IChannel }): JSX.Element => {
  const styles = buildStyles();

  return (
    <List.Item
      style={[styles.listItem]}
      title={channel.title}
      titleStyle={[styles.title]}
      description={'Last message...'}
      onPress={() => {}}
      left={() => <Avatar.Icon size={LIST_ITEM_IMAGE_SIZE} icon="account" />}
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
