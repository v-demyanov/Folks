import { Avatar, List } from 'react-native-paper';
import { useState } from 'react';

import { LIST_ITEM_IMAGE_SIZE } from '../../../../../common/constants/icons.constants';
import buildStyles from './selectable-users-list-item.styles';
import ListCheckBox from '../../../../../common/components/list-check-box/list-check-box';
import ISelectableUsersListItemProps from '../../../models/selectable-users-list-item.props';

const SelectableUsersListItem = ({
  onPress,
  user,
}: ISelectableUsersListItemProps): JSX.Element => {
  const styles = buildStyles();
  const [isOnFocus, setIsOnFocus] = useState(false);

  return (
    <List.Item
      style={[styles.listItem]}
      title={user.userName}
      titleStyle={[styles.title]}
      description={user.status}
      left={() => (
        <>
          <Avatar.Icon size={LIST_ITEM_IMAGE_SIZE} icon="account"></Avatar.Icon>
          {user.isSelected ? <ListCheckBox isOnFocus={isOnFocus} /> : null}
        </>
      )}
      onPress={() => onPress(user)}
      onPressIn={() => setIsOnFocus(true)}
      onPressOut={() => setIsOnFocus(false)}
    />
  );
};

export default SelectableUsersListItem;
