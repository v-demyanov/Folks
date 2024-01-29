import { useMemo } from 'react';
import { View } from 'react-native';
import { useTheme, Text } from 'react-native-paper';

import buildStyles from './messages-list-footer.component.styles';
import { getUserFrendlyDateString } from '../../../../../common/helpers';
import { Theme } from '../../../../../themes/types/theme';
import { IMessagesListFooterProps } from '../../../models';

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
