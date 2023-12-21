import { IBaseModel } from '../../../common/models';

export default interface IUser extends IBaseModel {
  userName: string;
  email: string;
  status: string;
}
