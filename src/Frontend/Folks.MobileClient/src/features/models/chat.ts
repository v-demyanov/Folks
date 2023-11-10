import IAuditableModel from './auditable-model';
import IBaseModel from './base-model';
import IUser from './user';

export default interface IChat extends IBaseModel, IAuditableModel {
  title: string | null;
  avatartImagePath: string | null;
  members: IUser[];
}
