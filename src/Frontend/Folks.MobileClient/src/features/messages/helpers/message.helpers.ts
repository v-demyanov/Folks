import { MessagesConstants } from '..';
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
