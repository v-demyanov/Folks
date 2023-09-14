import { ScrollView, Text, View } from 'react-native';

import HomeSearchbar from '../../../components/home/searchbar/home-searchbar';

const UsersScreen = (): JSX.Element => {
  return (
    <View>
      <HomeSearchbar />
      <ScrollView>
        <Text>Chats screen</Text>
      </ScrollView>
    </View>
  );
};

export default UsersScreen;
