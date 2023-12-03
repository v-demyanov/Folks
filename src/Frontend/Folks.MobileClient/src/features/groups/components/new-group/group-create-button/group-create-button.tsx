import { FAB } from 'react-native-paper';

import IGroupCreateButtonProps from '../../../models/group-create-button.props';

const GroupCreateButton = ({
  onPress,
  disabled,
}: IGroupCreateButtonProps): JSX.Element => {
  return (
    <FAB
      icon="check"
      style={{ position: 'absolute', bottom: 16, right: 16 }}
      onPress={() => onPress()}
      disabled={disabled}
    />
  );
};

export default GroupCreateButton;
