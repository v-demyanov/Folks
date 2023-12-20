import IBaseModel from '../../../common/models/base-model';

export default interface IUser extends IBaseModel {
  userName: string;
  email: string;
  status: string;
}
