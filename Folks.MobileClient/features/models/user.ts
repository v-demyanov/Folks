import IBaseModel from './base-model';

export default interface IUser extends IBaseModel {
  firstName: string;
  lastName: string;
  avatarImagePath: string | null;
}
