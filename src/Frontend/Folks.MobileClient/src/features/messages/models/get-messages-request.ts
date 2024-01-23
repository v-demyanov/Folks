import { ChannelType } from '../../channels/enums';

export default interface IGetMessagesRequest {
  channelId: string;
  channelType: ChannelType;
}
