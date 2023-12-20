import IUser from '../../users/models/user';

export default interface IMessagesListItem {
  id: string;
  content: string;
  sentAt: Date;
  channelId: string;
  owner: IUser;
  isLeftAlign: boolean;
}
