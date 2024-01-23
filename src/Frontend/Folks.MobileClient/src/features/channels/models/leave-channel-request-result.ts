import { ChannelType } from '../enums';

export default interface ILeaveChannelRequestResult {
  channelId: string;
  channelType: ChannelType;
  channelTitle: string | null;
}
