import { api } from '../../../api/api';
import IChannel from '../models/channel';

const channelsApi = api.injectEndpoints({
  endpoints: (builder) => ({
    getOwnChannels: builder.query<IChannel[], null>({
      query: () => ({ url: '/chatservice/channels', method: 'GET' }),
    }),
  }),
  overrideExisting: false,
});

export const { useGetOwnChannelsQuery } = channelsApi;
