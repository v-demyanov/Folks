import { ISectionListItem } from '../../../common/models';
import MessagesListItem from './messages-list-item';

export default interface IMessagesListProps {
  sections: ISectionListItem<Date, MessagesListItem>[];
}
