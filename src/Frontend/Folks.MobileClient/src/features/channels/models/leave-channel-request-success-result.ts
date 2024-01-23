import { LeaveChannelRequestInternalEvent } from '../enums';
import ILeaveChannelRequestResult from './leave-channel-request-result';

export default interface ILeaveChannelRequestSuccessResult
  extends ILeaveChannelRequestResult {
  events: LeaveChannelRequestInternalEvent[];
  recipients: string[];
}
