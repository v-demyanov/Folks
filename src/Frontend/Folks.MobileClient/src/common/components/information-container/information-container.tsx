import { useMemo } from 'react';
import { View } from 'react-native';
import { Text, useTheme } from 'react-native-paper';

import buildStyles from './information-container.styles';
import { Theme } from '../../../themes/types/theme';
import IInformationContainerProps from '../models/information-container.props';

const InformationContainer = ({
  message,
  backgroudColor,
}: IInformationContainerProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View
      style={
        backgroudColor
          ? { ...styles.view, backgroundColor: backgroudColor }
          : [styles.view]
      }
    >
      <Text variant="labelMedium" style={[styles.text]}>
        {message}
      </Text>
    </View>
  );
};

export default InformationContainer;
