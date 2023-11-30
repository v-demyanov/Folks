import { useTheme } from 'react-native-paper';
import Icon from 'react-native-vector-icons/MaterialIcons';

import { Theme } from '../../../themes/types/theme';

const ListCheckBox = ({ isOnFocus }: { isOnFocus: boolean }): JSX.Element => {
  const theme = useTheme<Theme>();

  return (
    <Icon
      name="check"
      color={'white'}
      size={18}
      style={{
        position: 'absolute',
        bottom: 0,
        left: 35,
        backgroundColor: theme.colors.check,
        borderRadius: 100,
        borderWidth: 1.5,
        borderColor: isOnFocus ? theme.colors.ripple : theme.colors.background,
        textAlign: 'center',
        verticalAlign: 'middle',
      }}
    />
  );
};

export default ListCheckBox;
