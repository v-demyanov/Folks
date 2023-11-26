import { FAB } from 'react-native-paper';
import { useState } from 'react';

const ChannelCreateButton = (): JSX.Element => {
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
          onPress: () => {},
        },
      ]}
      onStateChange={onStateChange}
    />
  );
};

export default ChannelCreateButton;
