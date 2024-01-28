import { IBaseModel } from '../../../common/models';
import { IMessage } from '../../messages/models';
import { ChannelType } from '../enums';

export default interface IChannel extends IBaseModel {
  title: string;
  type: ChannelType;
  createdAt: string;
  lastMessage: IMessage | null;
  unreadMessagesCount: number;
}
