import { HttpStatusCode } from 'axios';

import { api } from '../../../api/api';
import { channelsHubConnection } from '../../signalr/connections';
import {
  ICreateMessageRequest,
  IGetMessagesRequest,
  IMessage,
  IReadMessageContentsRequest,
} from '../models';
import { ChannelsHubEventsConstants } from '../../../api/constants';

const messagesApi = api.injectEndpoints({
  endpoints: (builder) => ({
    getMessages: builder.query<IMessage[], IGetMessagesRequest>({
      query: (arg) => ({
        url: `/channelsservice/channels/${arg.channelId}/messages?channelType=${arg.channelType}`,
        method: 'GET',
      }),
      onCacheEntryAdded: async (
        arg,
        { updateCachedData, cacheDataLoaded, cacheEntryRemoved }
      ) => {
        try {
          await cacheDataLoaded;

          channelsHubConnection.on(
            ChannelsHubEventsConstants.MESSAGE_SENT,
            (message: IMessage) => {
              updateCachedData((draft) => {
                if (message.channelId === arg.channelId) {
                  draft.push(message);
                }
              });
            }
          );

          channelsHubConnection.on(
            ChannelsHubEventsConstants.MESSAGES_UPDATED,
            (messages: IMessage[]) => {
              updateCachedData((draft) => {
                for (const message of messages) {
                  const index = draft.findIndex(
                    (draftMessage) => draftMessage.id === message.id
                  );
                  if (index > -1) {
                    draft.splice(index, 1, message);
                  }
                }
              });
            }
          );
        } catch {}

        await cacheEntryRemoved;
      },
    }),
    sendMessage: builder.mutation<null, ICreateMessageRequest>({
      queryFn: async (arg) => {
        try {
          const result = await channelsHubConnection.invoke(
            ChannelsHubEventsConstants.SEND_MESSAGE,
            arg
          );
          return {
            data: result,
          };
        } catch (error) {
          return {
            error: {
              status: HttpStatusCode.InternalServerError,
              data: `${error}`,
            },
          };
        }
      },
    }),
    readMessageContents: builder.mutation<null, IReadMessageContentsRequest>({
      query: (arg) => ({
        url: `/channelsservice/channels/${arg.channelId}/messages/readContents?channelType=${arg.channelType}`,
        method: 'PUT',
        body: arg.messageIds,
      }),
    }),
  }),
  overrideExisting: false,
});

export const {
  useGetMessagesQuery,
  useSendMessageMutation,
  useReadMessageContentsMutation,
} = messagesApi;
