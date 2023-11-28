import IUser from './user';

export default interface ISelectableUsersListProps {
  onListItemPress: (user: IUser) => void;
  users: IUser[];
}
