import { ChannelType } from '../../channels/enums';

export default interface IGetMessagesQuery {
  channelId: string;
  channelType: ChannelType;
}
