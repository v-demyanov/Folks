import { FAB } from 'react-native-paper';
import { useState } from 'react';
import { useNavigation } from '@react-navigation/native';

import { StackNavigation } from '../../../../navigation/app-navigator';

const CreateChannelButton = (): JSX.Element => {
  const navigation = useNavigation<StackNavigation>();
  const [isOpen, setIsOpen] = useState<boolean>(false);

  const onStateChange = ({ open }: { open: boolean }) => setIsOpen(open);

  return (
    <FAB.Group
      open={isOpen}
      visible
      icon={isOpen ? 'close' : 'chat-plus-outline'}
      actions={[
        {
          icon: 'chat-outline',
          label: 'Chat',
          onPress: () => {},
        },
        {
          icon: 'account-group-outline',
          label: 'Group',
          onPress: () => navigation.navigate('CreateGroupScreen'),
        },
      ]}
      onStateChange={onStateChange}
    />
  );
};

export default CreateChannelButton;
