import { useMemo } from 'react';
import { View } from 'react-native';
import { Text, useTheme } from 'react-native-paper';

import buildStyles from './messages-list-empty.component.styles';
import { Theme } from '../../../../../themes/types/theme';

const MessagesListEmptyComponent = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={[styles.centeredView]}>
      <Text variant="labelMedium">
        Congratulations, you have created a group!
      </Text>
    </View>
  );
};

export default MessagesListEmptyComponent;
