import { IconButton } from 'react-native-paper';

import { CHANNEL_CREATE_BUTTON_ICON_SIZE } from '../../../common/constants/icons.constants';
import buildStyles from './channel-create-button.styles';

const ChannelCreateButton = (): JSX.Element => {
  const styles = buildStyles();

  return (
    <IconButton
      style={[styles.button]}
      icon="chat-plus-outline"
      size={CHANNEL_CREATE_BUTTON_ICON_SIZE}
      mode="contained"
    />
  );
};

export default ChannelCreateButton;
