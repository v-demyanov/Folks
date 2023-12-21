import { IMessage, IMessagesListItem } from '../models';

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
