import IChannel from './channel';
import { SelectableItem } from '../../../common/models';

export default interface IChannelsListItemProps {
  channel: SelectableItem<IChannel>;
  onPress?: (channel: SelectableItem<IChannel>) => void;
  onPressIn?: (channel: SelectableItem<IChannel>) => void;
  onPressOut?: (channel: SelectableItem<IChannel>) => void;
}
