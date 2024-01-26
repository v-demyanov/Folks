import {
  ISectionListItem,
  IViewableItemsChangedEventInfo,
} from '../../../common/models';
import MessagesListItem from './messages-list-item';

export default interface IMessagesListProps {
  sections: ISectionListItem<Date, MessagesListItem>[];
  onViewableItemsChanged?:
    | ((info: IViewableItemsChangedEventInfo) => void)
    | null;
}
