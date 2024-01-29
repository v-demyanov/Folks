import ILeaveChannelRequestResult from './leave-channel-request-result';
import { LeaveChannelRequestInternalEvent } from '../enums';

export default interface ILeaveChannelRequestSuccessResult
  extends ILeaveChannelRequestResult {
  events: LeaveChannelRequestInternalEvent[];
  recipients: string[];
}
