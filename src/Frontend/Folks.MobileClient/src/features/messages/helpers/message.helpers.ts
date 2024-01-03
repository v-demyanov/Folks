import { MessagesConstants } from '..';
import { MessageType } from '../enums';
import { IMessage } from '../models';

export function splitContentOnFragments(content: string): string[] {
  const matcher = new RegExp(
    `.{1,${MessagesConstants.CONTENT_MAXIMUM_LENGTH}}`,
    'g'
  );

  return (
    content
      .match(matcher)
      ?.map((x) => x.trim())
      .filter((x) => x.length > 0) || []
  );
}

export function groupMessagesByDate(
  messages: IMessage[]
): Map<string, IMessage[]> {
  return messages.groupBy<string, IMessage>((message) => {
    const date = new Date(message.sentAt);
    date.setHours(0, 0, 0, 0);

    return date.toDateString();
  });
}

export function formatMessageContentByType(message: IMessage): string {
  switch (message.type) {
    case MessageType.Text:
      return message.content ?? '';
    case MessageType.NewGroupOwnerSetEvent:
      return 'New owner has been set';
    case MessageType.UserLeftEvent:
      return `${message.owner.userName} has left`;
    default:
      return '';
  }
}
