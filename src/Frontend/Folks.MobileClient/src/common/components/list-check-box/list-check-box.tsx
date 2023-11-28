import { useTheme } from 'react-native-paper';
import Icon from 'react-native-vector-icons/MaterialIcons';

import { Theme } from '../../../themes/types/theme';

const ListCheckBox = ({ isOnFocus }: { isOnFocus: boolean }): JSX.Element => {
  const theme = useTheme<Theme>();

  return (
    <Icon
      name="check-circle"
      color={theme.colors.check}
      size={21}
      style={{
        position: 'absolute',
        bottom: 0,
        left: 35,
        backgroundColor: isOnFocus
          ? theme.colors.ripple
          : theme.colors.background,
        borderRadius: 100,
        textAlign: 'center',
        verticalAlign: 'middle',
      }}
    />
  );
};

export default ListCheckBox;
