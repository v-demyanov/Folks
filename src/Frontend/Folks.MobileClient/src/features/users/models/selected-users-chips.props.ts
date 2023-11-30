import ISelectableUser from './selectable-user';

export default interface ISelectedUsersChipsProps {
  users: ISelectableUser[];
  onChipClose: (user: ISelectableUser) => void;
}
