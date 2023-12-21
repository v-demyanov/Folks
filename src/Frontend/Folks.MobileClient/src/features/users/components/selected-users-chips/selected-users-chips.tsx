import { ScrollView, View } from 'react-native';
import { Chip, Text, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import { ISelectedUsersChipsProps } from '../../models';
import { Theme } from '../../../../themes/types/theme';
import buildStyles from './selected-users-chips.styles';

const SelectedUsersChips = ({
  items,
  onChipClose,
}: ISelectedUsersChipsProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const renderChips = (): JSX.Element[] => {
    return items
      .filter((item) => item.isSelected)
      .map((item) => (
        <Chip
          style={[styles.chip]}
          icon="account"
          closeIcon="close"
          onPress={() => {}}
          onClose={() => onChipClose(item)}
          key={item.id}
        >
          {item.userName}
        </Chip>
      ));
  };

  return (
    <View>
      <ScrollView horizontal={true} style={[styles.scrollView]}>
        {items.filter((item) => item.isSelected).length ? (
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
