import { SelectableItem } from '../../../common/models';
import IUser from './user';

export default interface ISelectableUsersListItemProps {
  onPress: (item: SelectableItem<IUser>) => void;
  item: SelectableItem<IUser>;
}
