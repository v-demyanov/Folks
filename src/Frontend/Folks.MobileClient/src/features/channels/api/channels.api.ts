import { api } from '../../../api/api';
import { channelsHubConnection } from '../../signalr/connections';
import {
  IChannel,
  ILeaveChannelCommandErrorResult,
  ILeaveChannelCommandSuccessResult,
  ILeaveChannelRequest,
} from '../models';
import { ChannelsHubEventsConstants } from '../../../api/constants';

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

          channelsHubConnection.on(
            ChannelsHubEventsConstants.CHANNEL_CREATED,
            (channel: IChannel) => {
              updateCachedData((draft) => {
                draft.push(channel);
              });
            }
          );

          channelsHubConnection.on(
            ChannelsHubEventsConstants.CHANNEL_REMOVED,
            (channel: IChannel) => {
              updateCachedData((draft) => {
                const index = draft.findIndex(
                  (draftChannel) =>
                    draftChannel.id === channel.id &&
                    draftChannel.type === channel.type
                );
                if (index > -1) {
                  draft.splice(index, 1);
                }
              });
            }
          );

          channelsHubConnection.on(
            ChannelsHubEventsConstants.CHANNEL_UPDATED,
            (channel: IChannel) => {
              updateCachedData((draft) => {
                const index = draft.findIndex(
                  (draftChannel) =>
                    draftChannel.id === channel.id &&
                    draftChannel.type === channel.type
                );
                if (index > -1) {
                  draft.splice(index, 1, channel);
                }
              });
            }
          );
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
