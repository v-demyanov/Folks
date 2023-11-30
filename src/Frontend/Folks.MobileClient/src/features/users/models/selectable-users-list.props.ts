import ISelectableUser from './selectable-user';

export default interface ISelectableUsersListProps {
  onListItemPress: (user: ISelectableUser) => void;
  users: ISelectableUser[];
}
