import { useState } from 'react';
import { Avatar, List } from 'react-native-paper';

import buildStyles from './selectable-users-list-item.styles';
import { IconsConstants } from '../../../../../common';
import { ListCheckBox } from '../../../../../common/components';
import { ISelectableUsersListItemProps } from '../../../models';

const SelectableUsersListItem = ({
  onPress,
  item,
}: ISelectableUsersListItemProps): JSX.Element => {
  const styles = buildStyles();
  const [isOnFocus, setIsOnFocus] = useState(false);

  return (
    <List.Item
      style={[styles.listItem]}
      title={item.userName}
      titleStyle={[styles.title]}
      description={item.status}
      left={() => (
        <>
          <Avatar.Icon
            size={IconsConstants.LIST_ITEM_IMAGE_SIZE}
            icon="account"
          />
          {item.isSelected ? <ListCheckBox isOnFocus={isOnFocus} /> : null}
        </>
      )}
      onPress={() => onPress(item)}
      onPressIn={() => setIsOnFocus(true)}
      onPressOut={() => setIsOnFocus(false)}
    />
  );
};

export default SelectableUsersListItem;
