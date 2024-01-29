import { useNavigation } from '@react-navigation/native';
import { useMemo } from 'react';
import { Appbar, useTheme } from 'react-native-paper';

import buildStyles from './create-group-header.styles';
import { StackNavigation } from '../../../../../navigation/app-navigator';
import { Theme } from '../../../../../themes/types/theme';

const CreateGroupHeader = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);
  const navigation = useNavigation<StackNavigation>();

  return (
    <Appbar.Header style={[styles.header]}>
      <Appbar.BackAction
        color={theme.colors.onPrimary}
        onPress={() => navigation.goBack()}
      />
      <Appbar.Content title="New group" titleStyle={[styles.content]} />
    </Appbar.Header>
  );
};

export default CreateGroupHeader;
