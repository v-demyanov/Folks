import { api } from '../../../api/api';
import { IChannel } from '../../channels/models';
import { ICreateGroupRequest } from '../models';

const groupsApi = api.injectEndpoints({
  endpoints: (builder) => ({
    createGroup: builder.mutation<IChannel, ICreateGroupRequest>({
      query: (arg) => ({
        url: '/channelsservice/channels/groups',
        method: 'POST',
        body: arg,
      }),
    }),
  }),
  overrideExisting: false,
});

export const { useCreateGroupMutation } = groupsApi;
