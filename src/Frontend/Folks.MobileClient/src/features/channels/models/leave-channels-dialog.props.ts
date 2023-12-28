import { SelectableItem } from '../../../common/models';
import IChannel from './channel';

export default interface ILeaveChannelsDialogProps {
  visible: boolean;
  cancelButtonDisabled?: boolean;
  leaveButtonDisabled?: boolean;
  channels: SelectableItem<IChannel>[];
  onDismiss: () => void;
  onConfirmPress: () => void;
}
