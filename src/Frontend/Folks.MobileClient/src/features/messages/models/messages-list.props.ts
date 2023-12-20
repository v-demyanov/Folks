import ISectionListItem from '../../../common/models/section-list-item';
import IMessagesListItem from './messages-list-item';

export default interface IMessagesListProps {
  sections: ISectionListItem<Date, IMessagesListItem>[];
}
