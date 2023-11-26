import IBaseModel from '../../../common/models/base-model';
import ChannelType from '../enums/channel-type';

export default interface IChannel extends IBaseModel {
  title?: string;
  type: ChannelType;
}
