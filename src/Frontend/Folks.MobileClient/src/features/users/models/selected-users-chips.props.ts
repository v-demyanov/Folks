import IUser from './user';
import { SelectableItem } from '../../../common/models';

export default interface ISelectedUsersChipsProps {
  items: SelectableItem<IUser>[];
  onChipClose: (item: SelectableItem<IUser>) => void;
}
