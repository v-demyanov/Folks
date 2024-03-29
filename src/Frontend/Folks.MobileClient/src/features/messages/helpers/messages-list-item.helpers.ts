import { formatMessageContentByType } from './message.helpers';
import { IMessage, MessagesListItem } from '../models';

export function mapMessagesListItem(
  message: IMessage,
  currentUserId: string,
): MessagesListItem {
  return {
    ...message,
    sentAt: new Date(message.sentAt),
    isLeftAlign: message.owner.id !== currentUserId,
    content: formatMessageContentByType(message),
  };
}
