import IUser from './user';
import { SelectableItem } from '../../../common/models';

export default interface ISelectableUsersListItemProps {
  onPress: (item: SelectableItem<IUser>) => void;
  item: SelectableItem<IUser>;
}
