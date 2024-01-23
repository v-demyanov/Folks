import { ChannelType } from '../../channels/enums';

export default interface ICreateMessageRequest {
  ownerId: string;
  channelId: string;
  channelType: ChannelType;
  content: string;
  sentAt: Date;
}
