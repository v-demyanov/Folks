import { HttpStatusCode } from 'axios';

import { api } from '../../../api/api';
import { channelsHubConnection } from '../../signalr/connections';
import ICreateMessageCommand from '../models/create-message-command';
import IGetMessagesQuery from '../models/get-messages-query';
import IMessage from '../models/message';

const messagesApi = api.injectEndpoints({
  endpoints: (builder) => ({
    getMessages: builder.query<IMessage[], IGetMessagesQuery>({
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

          channelsHubConnection.on('ReceiveMessage', (message: IMessage) => {
            updateCachedData((draft) => {
              draft.push(message);
            });
          });
        } catch {}

        await cacheEntryRemoved;
      },
    }),
    sendMessage: builder.mutation<null, ICreateMessageCommand>({
      queryFn: async (arg) => {
        try {
          const result = await channelsHubConnection.invoke('SendMessage', arg);
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
  }),
  overrideExisting: false,
});

export const { useGetMessagesQuery, useSendMessageMutation } = messagesApi;
