import IMessage from './message';

type MessagesListItem = {
  isLeftAlign: boolean;
  sentAt: Date;
  content: string;
} & Omit<IMessage, 'sentAt' | 'content'>;

export default MessagesListItem;
