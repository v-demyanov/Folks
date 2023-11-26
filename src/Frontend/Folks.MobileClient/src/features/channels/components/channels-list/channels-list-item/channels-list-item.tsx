import { Avatar, List, Text } from 'react-native-paper';

import { CHANNELS_LIST_ITEM_IMAGE_SIZE } from '../../../../../common/constants/icons.constants';
import buildStyles from './channels-list-item.styles';
import IChannel from '../../../models/channel';

const ChannelsListItem = ({ channel }: { channel: IChannel }): JSX.Element => {
  const styles = buildStyles();

  return (
    <List.Item
      style={[styles.listItem]}
      title={channel.title}
      left={() => (
        <Avatar.Icon size={CHANNELS_LIST_ITEM_IMAGE_SIZE} icon="account" />
      )}
      right={() => <Text>Additional info</Text>}
    />
  );
};

export default ChannelsListItem;
