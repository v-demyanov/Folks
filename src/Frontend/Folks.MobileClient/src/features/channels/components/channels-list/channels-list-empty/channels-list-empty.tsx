import { View } from 'react-native';
import { Text, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import buildStyles from './channels-list-empty.styles';
import { Theme } from '../../../../../themes/types/theme';

const ChannelsListEmpty = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={[styles.centeredView]}>
      <Text variant="labelMedium">Create your first group or chat!</Text>
    </View>
  );
};

export default ChannelsListEmpty;
