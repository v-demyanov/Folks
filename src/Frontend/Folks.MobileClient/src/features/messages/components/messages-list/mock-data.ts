import ISectionListItem from '../../../../common/models/section-list-item';
import IMessagesListItem from '../../models/messages-list-item';

const generateMessages = (
  startIndex: number,
  sentAt: Date
): IMessagesListItem[] =>
  [...Array(5).keys()]
    .map(
      (id) =>
        ({
          id: (startIndex + id).toString(),
          content: `Message ${startIndex + id}`,
          isLeftAlign: (startIndex + id) % 2 === 0,
          userName: `User ${startIndex + id}`,
          sentAt: new Date(sentAt.getTime() + id),
        } as IMessagesListItem)
    )
    .sort((item1, item2) => item2.sentAt.getTime() - item1.sentAt.getTime());

const mockData = [
  {
    footer: new Date(),
    data: generateMessages(10, new Date()),
  } as ISectionListItem<Date, IMessagesListItem>,
  {
    footer: new Date(2023, 11, 11),
    data: generateMessages(5, new Date(2023, 11, 11)),
  } as ISectionListItem<Date, IMessagesListItem>,
  {
    footer: new Date(2023, 0, 4),
    data: generateMessages(0, new Date(2023, 0, 4)),
  } as ISectionListItem<Date, IMessagesListItem>,
];

export default mockData;
