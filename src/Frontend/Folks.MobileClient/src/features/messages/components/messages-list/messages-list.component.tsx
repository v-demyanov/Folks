import { useCallback, useMemo, useState } from 'react';
import { SafeAreaView, SectionList, SectionListData } from 'react-native';
import { useTheme } from 'react-native-paper';

import MessagesListEmptyComponent from './messages-list-empty/messages-list-empty.component';
import MessagesListFooterComponent from './messages-list-footer/messages-list-footer.component';
import MessagesListItemComponent from './messages-list-item/messages-list-item.component';
import buildStyles from './messages-list.component.styles';
import {
  ISectionListItem,
  IViewableItemsChangedEventInfo,
} from '../../../../common/models';
import { Theme } from '../../../../themes/types/theme';
import { IMessagesListProps, MessagesListItem } from '../../models';

const MessagesListComponent = ({
  sections,
  onViewableItemsChanged,
}: IMessagesListProps): JSX.Element => {
  const theme = useTheme<Theme>();
  const styles = useMemo(() => buildStyles(theme), [theme]);

  const [currentSectionFooter, setCurrentSectionFooter] = useState<Date | null>(
    null,
  );
  const [isScrolling, setIsScrolling] = useState<boolean>(false);
  const [timerId, setTimerId] = useState<NodeJS.Timeout | null>(null);

  const renderItem = ({ item }: { item: MessagesListItem }) => (
    <MessagesListItemComponent item={item} />
  );

  const renderSectionFooter = ({
    section: { footer },
  }: {
    section: SectionListData<
      MessagesListItem,
      ISectionListItem<Date, MessagesListItem>
    >;
  }): JSX.Element => <MessagesListFooterComponent content={footer} />;

  const renderStickyFooter = () =>
    currentSectionFooter ? (
      <MessagesListFooterComponent
        content={currentSectionFooter}
        wrapperStyle={[styles.stickyFooter]}
      />
    ) : null;

  const handleViewableItemsChanged = useCallback(
    (info: IViewableItemsChangedEventInfo): void => {
      if (!info.viewableItems || !info.viewableItems.length) {
        return;
      }

      const lastItem = info.viewableItems.pop();
      if (lastItem && lastItem.section) {
        setCurrentSectionFooter(lastItem.section.footer);
      }

      if (onViewableItemsChanged) {
        onViewableItemsChanged(info);
      }
    },
    [],
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
    return <MessagesListEmptyComponent />;
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
        inverted
      />
    </SafeAreaView>
  );
};

export default MessagesListComponent;
