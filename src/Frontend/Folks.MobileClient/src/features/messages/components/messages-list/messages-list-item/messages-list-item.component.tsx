import { View } from 'react-native';
import { Avatar, Text, useTheme } from 'react-native-paper';
import { useMemo } from 'react';
import { BlurView } from 'expo-blur';

import { IMessagesListItemComponentProps } from '../../../models';
import { Theme } from '../../../../../themes/types/theme';
import MessageContainerComponent from '../../message-container/message-container.component';
import { formatInHHMM } from '../../../../../common/helpers';
import { MessageType } from '../../../enums';
import buildStyles from './messages-list-item.component.styles';

const MessagesListItemComponent = ({
  item,
}: IMessagesListItemComponentProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  if (item.type !== MessageType.Text) {
    return (
      <BlurView style={[styles.specificMessageView]} intensity={50}>
        <Text style={[styles.contentText]} variant="titleSmall">
          {item.content}
        </Text>
      </BlurView>
    );
  }

  return (
    <View
      style={item.isLeftAlign ? [styles.wrapperLeft] : [styles.wrapperRight]}
    >
      <View style={[styles.wrapperLevel2]}>
        {item.isLeftAlign ? (
          <Avatar.Icon size={40} icon="account" style={[styles.avatar]} />
        ) : null}

        <MessageContainerComponent isLeftAlign={item.isLeftAlign}>
          {item.isLeftAlign ? (
            <Text variant="titleSmall" style={[styles.userNameText]}>
              {item.owner.userName}
            </Text>
          ) : null}
          <Text style={[styles.contentText]} variant="bodyLarge">
            {item.content}
          </Text>
        </MessageContainerComponent>
      </View>
      <Text style={[styles.messageSentAtText]} variant="bodySmall">
        {formatInHHMM(item.sentAt)}
      </Text>
    </View>
  );
};

export default MessagesListItemComponent;
