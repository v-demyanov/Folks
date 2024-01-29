import { useMemo } from 'react';
import { View } from 'react-native';
import { Text, useTheme } from 'react-native-paper';

import buildStyles from './selectable-users-list-empty.styles';
import { Theme } from '../../../../../themes/types/theme';

const SelectableUsersListEmpty = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={[styles.centeredView]}>
      <Text variant="labelMedium">Invite in Folks your family or friends!</Text>
    </View>
  );
};

export default SelectableUsersListEmpty;
