import { useState } from 'react';
import { TextInput } from 'react-native';
import { useTheme } from 'react-native-paper';

import { Theme } from '../../../../themes/types/theme';

const ChatInput = (): JSX.Element => {
  const [message, setMessage] = useState('');
  const theme = useTheme<Theme>();

  return (
    <TextInput
      style={{
        borderRadius: 20,
        backgroundColor: theme.colors.background,
        color: theme.colors.onBackground,
        paddingLeft: 10,
        flex: 1,
      }}
      placeholder="Type a message"
      value={message}
      onChangeText={(message) => setMessage(message)}
    />
  );
};

export default ChatInput;
