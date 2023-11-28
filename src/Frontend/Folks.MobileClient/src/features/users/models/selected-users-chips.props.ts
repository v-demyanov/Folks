import IUser from './user';

export default interface ISelectedUsersChipsProps {
  users: IUser[];
  onChipClose: (user: IUser) => void;
}
