import {
  SafeAreaView,
  SectionList,
  SectionListData,
  ViewToken,
} from 'react-native';
import { useTheme } from 'react-native-paper';
import { useMemo, useState } from 'react';

import { Theme } from '../../../../themes/types/theme';
import MessagesListItem from './messages-list-item/messages-list-item';
import IMessagesListItem from '../../models/messages-list-item';
import ISectionListItem from '../../../../common/models/section-list-item';
import MessagesListFooter from './messages-list-footer/messages-list-footer';
import buildStyles from './messages-list.styles';
import mockData from './mock-data';
import MessagesListEmpty from './messages-list-empty/messages-list-empty';

const MessagesList = (): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const [currentSectionFooter, setCurrentSectionFooter] = useState<Date | null>(
    null
  );
  const [isScrolling, setIsScrolling] = useState<boolean>(false);
  const [timerId, setTimerId] = useState<NodeJS.Timeout | null>(null);
  const [sections, setSections] =
    useState<ISectionListItem<Date, IMessagesListItem>[]>(mockData);

  const renderItem = ({ item }: { item: IMessagesListItem }) => (
    <MessagesListItem item={item} />
  );

  const renderSectionFooter = ({
    section: { footer: footer },
  }: {
    section: SectionListData<
      IMessagesListItem,
      ISectionListItem<Date, IMessagesListItem>
    >;
  }): JSX.Element => <MessagesListFooter content={footer} />;

  const renderStickyFooter = () =>
    currentSectionFooter ? (
      <MessagesListFooter
        content={currentSectionFooter}
        blurViewstyle={[styles.stickyFooter]}
      />
    ) : null;

  const handleViewableItemsChanged = ({
    viewableItems,
    changed,
  }: {
    viewableItems: Array<ViewToken>;
    changed: Array<ViewToken>;
  }): void => {
    if (!viewableItems || !viewableItems.length) {
      return;
    }

    const lastItem = viewableItems.pop();
    if (lastItem && lastItem.section) {
      setCurrentSectionFooter(lastItem.section.footer);
    }
  };

  const handleScroll = (): void => {
    if (timerId) {
      clearTimeout(timerId);
    }
    setIsScrolling(true);
  };

  const handleMomentumScrollEnd = (): void => {
    if (timerId) {
      clearTimeout(timerId);
    }
    const newTimerId = setTimeout(() => setIsScrolling(false), 500);
    setTimerId(newTimerId);
  };

  return (
    <SafeAreaView style={[styles.safeAreaView]}>
      {isScrolling ? renderStickyFooter() : null}
      <SectionList
        sections={sections}
        style={[styles.sectionList]}
        keyExtractor={(item, index) => item.id + index}
        renderItem={renderItem}
        renderSectionFooter={renderSectionFooter}
        onViewableItemsChanged={handleViewableItemsChanged}
        onScroll={handleScroll}
        onMomentumScrollEnd={handleMomentumScrollEnd}
        ListEmptyComponent={<MessagesListEmpty />}
        inverted={!!sections.length}
      />
    </SafeAreaView>
  );
};

export default MessagesList;
