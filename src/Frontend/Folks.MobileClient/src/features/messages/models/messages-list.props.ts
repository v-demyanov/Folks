import MessagesListItem from './messages-list-item';
import {
  ISectionListItem,
  IViewableItemsChangedEventInfo,
} from '../../../common/models';

export default interface IMessagesListProps {
  sections: ISectionListItem<Date, MessagesListItem>[];
  onViewableItemsChanged?:
    | ((info: IViewableItemsChangedEventInfo) => void)
    | null;
}
