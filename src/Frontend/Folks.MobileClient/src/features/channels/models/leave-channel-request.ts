import { ChannelType } from '../enums';

export default interface ILeaveChannelRequest {
  channelId: string;
  channelType: ChannelType;
}
