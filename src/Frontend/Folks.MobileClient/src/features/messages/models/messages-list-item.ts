import { IUser } from '../../users/models';

export default interface IMessagesListItem {
  id: string;
  content: string;
  sentAt: Date;
  channelId: string;
  owner: IUser;
  isLeftAlign: boolean;
  isSpecific: boolean;
}
