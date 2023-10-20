import { Avatar, List, Text } from 'react-native-paper';
import { GestureResponderEvent, StyleSheet } from 'react-native';
import { useNavigation } from '@react-navigation/native';

import { CHAT_LIST_ITEM_IMAGE_SIZE } from '../../../../common/constants/icons.constants';
import { getUserFrendlyDateString } from '../../../../common/helpers/date-helpers';
import { StackNavigation } from '../../../../navigation/app-navigator';

const styles = StyleSheet.create({
  listItem: {
    paddingLeft: 10,
  },
});

export interface ChatsListItemProps {
  title: string;
  imagePath: string;
  lastUpdateDate: Date;
}

const ChatsListItem = ({
  title,
  imagePath,
  lastUpdateDate,
}: ChatsListItemProps): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();

  const handlePress = (event: GestureResponderEvent) => {
    navigation.navigate('Chat');
  };

  return (
    <List.Item
      style={[styles.listItem]}
      title={title}
      left={() => (
        <Avatar.Icon size={CHAT_LIST_ITEM_IMAGE_SIZE} icon="account" />
      )}
      right={() => <Text>{getUserFrendlyDateString(lastUpdateDate)}</Text>}
      onPress={handlePress}
    />
  );
};

export default ChatsListItem;
