import { api } from '../../../api/api';
import { channelsHubConnection } from '../../signalr/connections';
import { IChannel } from '../models';

const channelsApi = api.injectEndpoints({
  endpoints: (builder) => ({
    getOwnChannels: builder.query<IChannel[], null>({
      query: () => ({ url: '/channelsservice/channels', method: 'GET' }),
      onCacheEntryAdded: async (
        arg,
        { updateCachedData, cacheDataLoaded, cacheEntryRemoved }
      ) => {
        try {
          await cacheDataLoaded;

          channelsHubConnection.on('ReceiveChannel', (channel: IChannel) => {
            updateCachedData((draft) => {
              draft.push(channel);
            });
          });
        } catch {}

        await cacheEntryRemoved;
      },
    }),
  }),
  overrideExisting: false,
});

export const { useGetOwnChannelsQuery } = channelsApi;
