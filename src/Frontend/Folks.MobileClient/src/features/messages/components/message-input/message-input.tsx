import { TextInput, View } from 'react-native';
import { IconButton, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import { SEND_MESSAGE_BUTTON_ICON_SIZE } from '../../../../common/constants/icons.constants';
import { Theme } from '../../../../themes/types/theme';
import buildStyles from './message-input.styles';

const MessageInput = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={[styles.wrapper]}>
      <TextInput
        style={[styles.textInput]}
        placeholder={'Message'}
        placeholderTextColor={theme.colors.secondary}
      />
      <IconButton
        icon="send"
        size={SEND_MESSAGE_BUTTON_ICON_SIZE}
        onPress={() => {}}
      />
    </View>
  );
};

export default MessageInput;
