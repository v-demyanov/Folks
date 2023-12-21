import { TextInput, View } from 'react-native';
import { IconButton, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import { IconsConstants } from '../../../../common';
import { Theme } from '../../../../themes/types/theme';
import buildStyles from './message-input.styles';
import { IMessageInputProps } from '../../models';

const MessageInput = ({
  form,
  onSendPress,
  sendDisabled,
}: IMessageInputProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={[styles.wrapper]}>
      <TextInput
        style={[styles.textInput]}
        placeholder={'Message'}
        placeholderTextColor={theme.colors.secondary}
        value={form.values.content}
        onChangeText={form.handleChange('content')}
        onBlur={form.handleBlur('content')}
      />
      <IconButton
        icon="send"
        size={IconsConstants.SEND_MESSAGE_BUTTON_ICON_SIZE}
        onPress={onSendPress}
        disabled={sendDisabled}
      />
    </View>
  );
};

export default MessageInput;
