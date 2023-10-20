import { Appbar } from 'react-native-paper';
import { StackNavigation } from '../../../navigation/app-navigator';
import { useNavigation } from '@react-navigation/native';

const AccessHeader = (): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();

  return (
    <Appbar.Header>
      <Appbar.BackAction onPress={() => navigation.goBack()} />
    </Appbar.Header>
  );
};

export default AccessHeader;
