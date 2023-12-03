import { View } from 'react-native';
import { Avatar, TextInput, useTheme } from 'react-native-paper';
import { useMemo } from 'react';

import NewGroupFormProps from '../../../models/new-group-form.props';
import { Theme } from '../../../../../themes/types/theme';
import buildStyles from './new-group-form.styles';

const NewGroupForm = ({ form }: NewGroupFormProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  return (
    <View style={[styles.wrapper]}>
      <View style={[styles.groupImageView]}>
        <Avatar.Icon size={70} icon="image-plus" />
      </View>
      <View style={[styles.inputView]}>
        <TextInput
          label="Group title"
          style={[styles.textInput]}
          value={form.values.title}
          onChangeText={form.handleChange('title')}
          onBlur={form.handleBlur('title')}
        />
      </View>
    </View>
  );
};

export default NewGroupForm;
