import IUser from './user';

export default interface ISelectableUsersListItemProps {
  onPress: (user: IUser) => void;
  user: IUser;
}
