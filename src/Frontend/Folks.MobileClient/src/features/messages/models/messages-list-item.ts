import { IUser } from '../../users/models';
import { MessageType } from '../enums';

export default interface IMessagesListItem {
  id: string;
  content: string;
  sentAt: Date;
  channelId: string;
  owner: IUser;
  type: MessageType;
  isLeftAlign: boolean;
}
