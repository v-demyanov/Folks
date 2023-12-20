import ChannelType from '../../channels/enums/channel-type';

export default interface IGetMessagesQuery {
  channelId: string;
  channelType: ChannelType;
}
