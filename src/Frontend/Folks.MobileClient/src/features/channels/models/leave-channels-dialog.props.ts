import IChannel from './channel';
import { SelectableItem } from '../../../common/models';

export default interface ILeaveChannelsDialogProps {
  visible: boolean;
  cancelButtonDisabled?: boolean;
  leaveButtonDisabled?: boolean;
  channels: SelectableItem<IChannel>[];
  onDismiss: () => void;
  onConfirmPress: () => void;
}
