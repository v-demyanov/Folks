import { NativeStackScreenProps } from '@react-navigation/native-stack';

import GroupHeader from '../../../features/groups/components/group-header/group-header';
import MessageInput from '../../../features/messages/components/message-input/message-input';
import MessagesList from '../../../features/messages/components/messages-list/messages-list';
import { RootStackParamList } from '../../../navigation/app-navigator';

type Props = NativeStackScreenProps<RootStackParamList, 'Group'>;

const GroupScreen = ({ route }: Props): JSX.Element => {
  const group = route.params;

  return (
    <>
      <GroupHeader group={group} />
      <MessagesList />
      <MessageInput />
    </>
  );
};

export default GroupScreen;
