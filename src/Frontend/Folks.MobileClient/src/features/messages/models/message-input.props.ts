import { FormikProps } from 'formik';

import ISendMessageFormValue from './send-message-form-value';

export default interface IMessageInputProps {
  form: FormikProps<ISendMessageFormValue>;
  onSendPress: () => void;
  sendDisabled: boolean;
}
