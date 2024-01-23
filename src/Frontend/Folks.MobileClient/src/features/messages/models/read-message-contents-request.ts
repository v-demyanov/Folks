import { ChannelType } from '../../channels/enums';

export default interface IReadMessageContentsRequest {
  messageIds: string[];
  channelId: string;
  channelType: ChannelType;
}
