import { BlurView } from 'expo-blur';
import { useTheme, Text } from 'react-native-paper';
import { useMemo } from 'react';

import { Theme } from '../../../../../themes/types/theme';
import { getUserFrendlyDateString } from '../../../../../common/helpers';
import { IMessagesListFooterProps } from '../../../models';
import buildStyles from './messages-list-footer.component.styles';

const MessagesListFooterComponent = ({
  content,
  blurViewstyle,
}: IMessagesListFooterProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <BlurView style={[styles.blurView, blurViewstyle]} intensity={50}>
      <Text style={[styles.text]} variant="titleSmall">
        {getUserFrendlyDateString(content)}
      </Text>
    </BlurView>
  );
};

export default MessagesListFooterComponent;
