import IMessage from '../models/message';
import IMessagesListItem from '../models/messages-list-item';

export function mapMessagesListItem(
  message: IMessage,
  currentUserId: string
): IMessagesListItem {
  return {
    ...message,
    sentAt: new Date(message.sentAt),
    isLeftAlign: message.owner.id !== currentUserId,
  };
}
