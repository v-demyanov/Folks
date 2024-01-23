import {
  SafeAreaView,
  SectionList,
  SectionListData,
  ViewToken,
} from 'react-native';
import { useTheme } from 'react-native-paper';
import { useCallback, useMemo, useState } from 'react';

import { Theme } from '../../../../themes/types/theme';
import MessagesListItemComponent from './messages-list-item-component/messages-list-item-component';
import { ISectionListItem } from '../../../../common/models';
import MessagesListFooter from './messages-list-footer/messages-list-footer';
import buildStyles from './messages-list.styles';
import MessagesListEmpty from './messages-list-empty/messages-list-empty';
import { IMessagesListProps, MessagesListItem } from '../../models';

const MessagesList = ({ sections }: IMessagesListProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const [currentSectionFooter, setCurrentSectionFooter] = useState<Date | null>(
    null
  );
  const [isScrolling, setIsScrolling] = useState<boolean>(false);
  const [timerId, setTimerId] = useState<NodeJS.Timeout | null>(null);

  const renderItem = ({ item }: { item: MessagesListItem }) => (
    <MessagesListItemComponent item={item} />
  );

  const renderSectionFooter = ({
    section: { footer: footer },
  }: {
    section: SectionListData<
      MessagesListItem,
      ISectionListItem<Date, MessagesListItem>
    >;
  }): JSX.Element => <MessagesListFooter content={footer} />;

  const renderStickyFooter = () =>
    currentSectionFooter ? (
      <MessagesListFooter
        content={currentSectionFooter}
        blurViewstyle={[styles.stickyFooter]}
      />
    ) : null;

  const handleViewableItemsChanged = useCallback(
    ({
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
    },
    []
  );

  const handleScroll = (): void => {
    if (timerId) {
      clearTimeout(timerId);
    }
    setIsScrolling(true);
  };

  const handleMomentumScrollEnd = useCallback((): void => {
    if (timerId) {
      clearTimeout(timerId);
    }
    const newTimerId = setTimeout(() => setIsScrolling(false), 500);
    setTimerId(newTimerId);
  }, []);

  if (!sections.length) {
    return <MessagesListEmpty />;
  }

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
        inverted={!!sections.length}
      />
    </SafeAreaView>
  );
};

export default MessagesList;
