import { View } from 'react-native';
import { Text, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import buildStyles from './messages-list-empty.styles';
import { Theme } from '../../../../../themes/types/theme';

const MessagesListEmpty = (): JSX.Element => {
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

export default MessagesListEmpty;
