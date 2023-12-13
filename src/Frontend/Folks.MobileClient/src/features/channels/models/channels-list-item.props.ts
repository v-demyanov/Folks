import IChannel from './channel';

export default interface IChannelsListItemProps {
  channel: IChannel;
  onPress: (channel: IChannel) => void;
}
