import IUser from './user';
import { SelectableItem } from '../../../common/models';

export default interface ISelectableUsersListProps {
  onListItemPress: (item: SelectableItem<IUser>) => void;
  items: SelectableItem<IUser>[];
}
