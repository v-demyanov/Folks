import { api } from '../../../api/api';
import { channelsHubConnection } from '../../signalr/connections';
import {
  IChannel,
  ILeaveChannelCommandErrorResult,
  ILeaveChannelCommandSuccessResult,
  ILeaveChannelRequest,
} from '../models';

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

          channelsHubConnection.on('ChannelCreated', (channel: IChannel) => {
            updateCachedData((draft) => {
              draft.push(channel);
            });
          });

          channelsHubConnection.on('ChannelRemoved', (channel: IChannel) => {
            updateCachedData((draft) => {
              const index = draft.findIndex(
                (draftChannel) =>
                  draftChannel.id === channel.id &&
                  draftChannel.type === channel.type
              );
              draft.splice(index, 1);
            });
          });
        } catch {}

        await cacheEntryRemoved;
      },
    }),
    leaveChannels: builder.mutation<
      Array<
        ILeaveChannelCommandErrorResult | ILeaveChannelCommandSuccessResult
      >,
      ILeaveChannelRequest[]
    >({
      query: (arg) => ({
        url: '/channelsservice/channels/leave',
        method: 'POST',
        body: arg,
      }),
    }),
  }),
  overrideExisting: false,
});

export const { useGetOwnChannelsQuery, useLeaveChannelsMutation } = channelsApi;
