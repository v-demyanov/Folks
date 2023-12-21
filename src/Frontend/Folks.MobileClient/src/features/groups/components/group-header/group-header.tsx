import { useNavigation } from '@react-navigation/native';
import { Appbar, Text, Avatar, useTheme } from 'react-native-paper';
import { View } from 'react-native';
import { useMemo } from 'react';

import { StackNavigation } from '../../../../navigation/app-navigator';
import { IChannel } from '../../../channels/models';
import { IconsConstants } from '../../../../common';
import buildStyles from './group-header.styles';
import { Theme } from '../../../../themes/types/theme';

const GroupHeader = ({ group }: { group: IChannel }): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);
  const navigation = useNavigation<StackNavigation>();

  return (
    <Appbar.Header style={[styles.header]}>
      <Appbar.BackAction
        color={theme.colors.onPrimary}
        onPress={() => navigation.navigate('Home')}
      />
      <Appbar.Content
        title={
          <View style={[styles.contentView]}>
            <Avatar.Icon
              style={[styles.groupImage]}
              size={IconsConstants.GROUP_ICON_SIZE}
              icon="image"
            />
            <View style={[styles.groupDetailsView]}>
              <Text variant="titleMedium" style={[styles.groupTitle]}>
                {group.title}
              </Text>
              <Text style={[styles.members]}>3 members</Text>
            </View>
          </View>
        }
      />
      <Appbar.Action
        icon="phone"
        color={theme.colors.onPrimary}
        onPress={() => {}}
      />
    </Appbar.Header>
  );
};

export default GroupHeader;
