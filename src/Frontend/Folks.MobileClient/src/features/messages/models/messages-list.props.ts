import { ISectionListItem } from '../../../common/models';
import IMessagesListItem from './messages-list-item';

export default interface IMessagesListProps {
  sections: ISectionListItem<Date, IMessagesListItem>[];
}
