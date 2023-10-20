import { Appbar } from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';

import { StackNavigation } from '../../../navigation/app-navigator';

const ChatHeader = (): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();

  return (
    <Appbar.Header>
      <Appbar.BackAction onPress={() => navigation.goBack()} />
      <Appbar.Content title="Title" />
      <Appbar.Action icon="video" onPress={() => {}} />
      <Appbar.Action icon="phone" onPress={() => {}} />
    </Appbar.Header>
  );
};

export default ChatHeader;
