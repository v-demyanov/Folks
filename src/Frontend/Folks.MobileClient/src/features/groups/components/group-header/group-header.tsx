import { useNavigation } from '@react-navigation/native';
import { Appbar, Text, Avatar } from 'react-native-paper';
import { View } from 'react-native';

import { StackNavigation } from '../../../../navigation/app-navigator';
import IChannel from '../../../channels/models/channel';
import { GROUP_ICON_SIZE } from '../../../../common/constants/icons.constants';
import buildStyles from './group-header.styles';

const GroupHeader = ({ group }: { group: IChannel }): JSX.Element => {
  const styles = buildStyles();
  const navigation = useNavigation<StackNavigation>();

  return (
    <Appbar.Header>
      <Appbar.BackAction onPress={() => navigation.goBack()} />
      <Appbar.Content
        title={
          <View style={[styles.contentView]}>
            <Avatar.Icon
              style={[styles.groupImage]}
              size={GROUP_ICON_SIZE}
              icon="image"
            />
            <View style={[styles.groupDetailsView]}>
              <Text variant="titleMedium">{group.title}</Text>
              <Text>3 members</Text>
            </View>
          </View>
        }
      />
      <Appbar.Action icon="phone" onPress={() => {}} />
    </Appbar.Header>
  );
};

export default GroupHeader;
