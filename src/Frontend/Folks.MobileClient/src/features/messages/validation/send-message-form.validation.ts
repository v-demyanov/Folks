import * as Yup from 'yup';

const SendMessageFormValidationSchema = Yup.object().shape({
  content: Yup.string().trim().required('Field is required'),
});

export default SendMessageFormValidationSchema;
