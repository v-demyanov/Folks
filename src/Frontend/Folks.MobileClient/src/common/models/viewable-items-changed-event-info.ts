import { ViewToken } from 'react-native';

export default interface IViewableItemsChangedEventInfo {
  viewableItems: ViewToken[];
  changed: ViewToken[];
}
