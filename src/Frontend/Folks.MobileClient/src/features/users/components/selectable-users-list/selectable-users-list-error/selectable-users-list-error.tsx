import { View } from 'react-native';
import { Text, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import buildStyles from './selectable-users-list-error.styles';
import { Theme } from '../../../../../themes/types/theme';

const SelectableUsersListError = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={[styles.usersQueryError]}>
      <Text variant="labelMedium" style={[styles.usersQueryErrorText]}>
        Oops! Something went wrong,{'\n'} while users loading...
      </Text>
    </View>
  );
};

export default SelectableUsersListError;
