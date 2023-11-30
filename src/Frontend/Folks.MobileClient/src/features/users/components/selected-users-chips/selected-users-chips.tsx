import { ScrollView, View } from 'react-native';
import { Chip, Text, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import ISelectedUsersChipsProps from '../../models/selected-users-chips.props';
import { Theme } from '../../../../themes/types/theme';
import buildStyles from './selected-users-chips.styles';

const SelectedUsersChips = ({
  users,
  onChipClose,
}: ISelectedUsersChipsProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const renderChips = (): JSX.Element[] => {
    return users
      .filter((user) => user.isSelected)
      .map((user) => (
        <Chip
          style={[styles.chip]}
          icon="account"
          closeIcon="close"
          onPress={() => {}}
          onClose={() => onChipClose(user)}
          key={user.id}
        >
          {user.userName}
        </Chip>
      ));
  };

  return (
    <View>
      <ScrollView horizontal={true} style={[styles.scrollView]}>
        {users.filter((user) => user.isSelected).length ? (
          renderChips()
        ) : (
          <Text style={[styles.emptyView]} variant="titleMedium">
            Who would you like to add?
          </Text>
        )}
      </ScrollView>
    </View>
  );
};

export default SelectedUsersChips;
