import { ChannelType } from '../../channels/enums';

export default interface ICreateMessageCommand {
  ownerId: string;
  channelId: string;
  channelType: ChannelType;
  content: string;
  sentAt: Date;
}
