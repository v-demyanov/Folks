import ChannelType from '../../channels/enums/channel-type';

export default interface ICreateMessageCommand {
  ownerId: string;
  channelId: string;
  channelType: ChannelType;
  content: string;
  sentAt: Date;
}
