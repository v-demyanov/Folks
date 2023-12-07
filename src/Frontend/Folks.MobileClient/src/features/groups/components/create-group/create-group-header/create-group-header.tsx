import { useNavigation } from '@react-navigation/native';
import { Appbar } from 'react-native-paper';

import { StackNavigation } from '../../../../../navigation/app-navigator';

const CreateGroupHeader = (): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();

  return (
    <Appbar.Header>
      <Appbar.BackAction onPress={() => navigation.goBack()} />
      <Appbar.Content title="New group" titleStyle={{fontWeight: 'bold'}} />
    </Appbar.Header>
  );
};

export default CreateGroupHeader;
