import { View } from 'react-native';
import { Text } from 'react-native-paper';

import buildStyles from './selectable-users-list-empty.styles';

const SelectableUsersListEmpty = (): JSX.Element => {
  const styles = buildStyles();

  return (
    <View style={[styles.centeredView]}>
      <Text variant="labelMedium">Invite in Folks your family or friends!</Text>
    </View>
  );
};

export default SelectableUsersListEmpty;
