import { useTheme, Text } from 'react-native-paper';
import { useMemo } from 'react';
import { View } from 'react-native';

import { Theme } from '../../../../../themes/types/theme';
import { getUserFrendlyDateString } from '../../../../../common/helpers';
import { IMessagesListFooterProps } from '../../../models';
import buildStyles from './messages-list-footer.component.styles';

const MessagesListFooterComponent = ({
  content,
  wrapperStyle,
}: IMessagesListFooterProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={[styles.view, wrapperStyle]}>
      <Text style={[styles.text]} variant="titleSmall">
        {getUserFrendlyDateString(content)}
      </Text>
    </View>
  );
};

export default MessagesListFooterComponent;
