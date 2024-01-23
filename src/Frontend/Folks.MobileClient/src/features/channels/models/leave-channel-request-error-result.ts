import ILeaveChannelRequestResult from './leave-channel-request-result';

export default interface ILeaveChannelRequestErrorResult
  extends ILeaveChannelRequestResult {
  error: string;
}
