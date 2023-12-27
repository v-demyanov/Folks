import { LeaveChannelCommandInternalEvent } from '../enums';
import ILeaveChannelCommandResult from './leave-channel-command-result';

export default interface ILeaveChannelCommandSuccessResult
  extends ILeaveChannelCommandResult {
  events: LeaveChannelCommandInternalEvent[];
  recipients: string[];
}
