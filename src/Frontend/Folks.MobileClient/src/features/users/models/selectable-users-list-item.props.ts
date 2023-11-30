import ISelectableUser from './selectable-user';

export default interface ISelectableUsersListItemProps {
  onPress: (user: ISelectableUser) => void;
  user: ISelectableUser;
}
