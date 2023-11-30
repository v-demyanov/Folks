import IUser from './user';

export default interface ISelectableUser extends IUser {
  isSelected: boolean;
}
