import { ChannelType } from '../enums';

export default interface ILeaveChannelCommandResult {
  channelId: string;
  channelType: ChannelType;
  channelTitle: string | null;
}
