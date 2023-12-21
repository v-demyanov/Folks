import { IBaseModel } from '../../../common/models';
import { IUser } from '../../users/models';

export default interface IMessage extends IBaseModel {
  content: string;
  sentAt: string;
  channelId: string;
  owner: IUser;
}
