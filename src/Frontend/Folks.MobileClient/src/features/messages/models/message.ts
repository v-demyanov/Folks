import { IBaseModel } from '../../../common/models';
import { IUser } from '../../users/models';
import { MessageType } from '../enums';

export default interface IMessage extends IBaseModel {
  content: string | null;
  sentAt: string;
  channelId: string;
  owner: IUser;
  type: MessageType;
  readBy: IUser[];
}
