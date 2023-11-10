import { ScrollView } from 'react-native';
import { Text } from 'react-native-paper';

const messages = Array.from(Array(10).keys()).map((id) => (
  <Text key={id}>Message {id}</Text>
));

const ChatMessageList = (): JSX.Element => {
  return <ScrollView>{messages}</ScrollView>;
};

export default ChatMessageList;
