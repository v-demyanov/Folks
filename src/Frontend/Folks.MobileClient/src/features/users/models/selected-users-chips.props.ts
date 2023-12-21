import { SelectableItem } from '../../../common/models';
import IUser from './user';

export default interface ISelectedUsersChipsProps {
  items: SelectableItem<IUser>[];
  onChipClose: (item: SelectableItem<IUser>) => void;
}
