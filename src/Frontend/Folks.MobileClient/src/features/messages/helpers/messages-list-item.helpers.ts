import { IMessage, IMessagesListItem } from '../models';
import { formatMessageContentByType } from './message.helpers';

export function mapMessagesListItem(
  message: IMessage,
  currentUserId: string
): IMessagesListItem {
  return {
    ...message,
    sentAt: new Date(message.sentAt),
    isLeftAlign: message.owner.id !== currentUserId,
    content: formatMessageContentByType(message),
  };
}
