import { FAB } from 'react-native-paper';

import ICreateGroupButtonProps from '../../../models/create-group-button.props';

const CreateGroupButton = ({
  onPress,
  disabled,
}: ICreateGroupButtonProps): JSX.Element => {
  return (
    <FAB
      icon="check"
      style={{ position: 'absolute', bottom: 16, right: 16 }}
      onPress={() => onPress()}
      disabled={disabled}
    />
  );
};

export default CreateGroupButton;
