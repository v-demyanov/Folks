import IBaseModel from '../../../common/models/base-model';
import IUser from '../../users/models/user';

export default interface IMessage extends IBaseModel {
  content: string;
  sentAt: string;
  channelId: string;
  owner: IUser;
}
