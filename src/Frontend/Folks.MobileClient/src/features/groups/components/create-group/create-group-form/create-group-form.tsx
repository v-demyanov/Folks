import { View } from 'react-native';
import { Avatar, HelperText, TextInput, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import ICreateGroupFormProps from '../../../models/create-group-form.props';
import { Theme } from '../../../../../themes/types/theme';
import buildStyles from './create-group-form.styles';

const CreateGroupForm = ({ form }: ICreateGroupFormProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const isTitleError = (): boolean =>
    !!form.errors.title && !!form.touched.title;

  return (
    <View style={[styles.wrapper]}>
      <View style={[styles.groupImageView]}>
        <Avatar.Icon size={86} icon="image-plus" />
      </View>
      <View style={[styles.inputView]}>
        <TextInput
          label="Group title"
          style={[styles.textInput]}
          value={form.values.title}
          onChangeText={form.handleChange('title')}
          onBlur={form.handleBlur('title')}
          error={isTitleError()}
        />
        <HelperText type="error" visible={isTitleError()}>
          {form.errors.title}
        </HelperText>
      </View>
    </View>
  );
};

export default CreateGroupForm;
