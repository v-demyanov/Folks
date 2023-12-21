import { SelectableItem } from '../../../common/models';
import IUser from './user';

export default interface ISelectableUsersListProps {
  onListItemPress: (item: SelectableItem<IUser>) => void;
  items: SelectableItem<IUser>[];
}
