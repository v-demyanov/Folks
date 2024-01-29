import { useMemo } from 'react';
import { View } from 'react-native';
import { useTheme } from 'react-native-paper';
import Svg, { Path } from 'react-native-svg';

import buildStyles from './message-container.component.styles';
import { Theme } from '../../../../themes/types/theme';
import { IMessageContainerProps } from '../../models';

const MessageContainerComponent = ({
  isLeftAlign,
  children,
}: IMessageContainerProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={isLeftAlign ? [styles.wrapperLeft] : [styles.wrapperRight]}>
      <Svg
        viewBox="0 0 198 198"
        width={10}
        height={10}
        fill={
          isLeftAlign
            ? theme.colors.messageContainer
            : theme.colors.ownMessageContainer
        }
        style={isLeftAlign ? null : [styles.svg]}
      >
        <Path
          transform="rotate(-90 99 99)"
          d="m0,198.00001l0,-198.00002c33.00003,109.00001 69.00006,189.00001 198.00009,198.00002l-198.00009,0z"
        />
      </Svg>
      <View
        style={
          isLeftAlign
            ? [styles.contentWrapperLeft]
            : [styles.contentWrapperRight]
        }
      >
        {children}
      </View>
    </View>
  );
};

export default MessageContainerComponent;
