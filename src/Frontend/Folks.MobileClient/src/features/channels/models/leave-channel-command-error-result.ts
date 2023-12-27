import ILeaveChannelCommandResult from './leave-channel-command-result';

export default interface ILeaveChannelCommandErrorResult
  extends ILeaveChannelCommandResult {
  error: string;
}
