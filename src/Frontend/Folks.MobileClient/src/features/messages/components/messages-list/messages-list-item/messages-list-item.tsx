import { View } from 'react-native';
import { Avatar, Text, useTheme } from 'react-native-paper';
import { useMemo } from 'react';
import { BlurView } from 'expo-blur';

import { IMessagesListItemProps } from '../../../models';
import { Theme } from '../../../../../themes/types/theme';
import buildStyles from './messages-list-item.styles';
import MessageContainer from '../../message-container/message-container';
import { formatInHHMM } from '../../../../../common/helpers';

const MessagesListItem = ({ item }: IMessagesListItemProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  if (item.isSpecific) {
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

        <MessageContainer isLeftAlign={item.isLeftAlign}>
          {item.isLeftAlign ? (
            <Text variant="titleSmall" style={[styles.userNameText]}>
              {item.owner.userName}
            </Text>
          ) : null}
          <Text style={[styles.contentText]} variant="bodyLarge">
            {item.content}
          </Text>
        </MessageContainer>
      </View>
      <Text style={[styles.messageSentAtText]} variant="bodySmall">
        {formatInHHMM(item.sentAt)}
      </Text>
    </View>
  );
};

export default MessagesListItem;
