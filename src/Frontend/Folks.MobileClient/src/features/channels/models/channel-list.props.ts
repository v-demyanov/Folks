import { SelectableItem } from '../../../common/models';
import IChannel from './channel';

export default interface IChannelListProps {
  channels: SelectableItem<IChannel>[];
  onListItemPress?: (item: SelectableItem<IChannel>) => void;
  onListItemPressIn?: (item: SelectableItem<IChannel>) => void;
  onListItemPressOut?: (item: SelectableItem<IChannel>) => void;
}
