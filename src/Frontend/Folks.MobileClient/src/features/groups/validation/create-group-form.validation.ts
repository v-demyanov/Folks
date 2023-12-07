import * as Yup from 'yup';

import * as GroupConstants from '../constants/group.constants';

const CreateGroupFormValidationSchema = Yup.object().shape({
  title: Yup.string()
    .min(GroupConstants.TITLE_MINIMUM_LENGTH, `Minimum length is ${GroupConstants.TITLE_MINIMUM_LENGTH}`)
    .max(GroupConstants.TITLE_MAXIMUM_LENGTH, `Maximum length is ${GroupConstants.TITLE_MAXIMUM_LENGTH}`)
    .required('Field is required')
});

export default CreateGroupFormValidationSchema;
