import { Avatar, List, Text } from 'react-native-paper';

import { CHANNELS_LIST_ITEM_IMAGE_SIZE } from '../../../../common/constants/icons.constants';
import buildStyles from './channels-list-item.styles';

const ChannelsListItem = (): JSX.Element => {
  const styles = buildStyles();

  return (
    <List.Item
      style={[styles.listItem]}
      title={'Channel'}
      left={() => (
        <Avatar.Icon size={CHANNELS_LIST_ITEM_IMAGE_SIZE} icon="account" />
      )}
      right={() => <Text>Channel</Text>}
    />
  );
};

export default ChannelsListItem;
