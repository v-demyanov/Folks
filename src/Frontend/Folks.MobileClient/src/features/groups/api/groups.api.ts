import { api } from '../../../api/api';
import { IChannel } from '../../channels/models';
import { ICreateGroupCommand } from '../models';

const groupsApi = api.injectEndpoints({
  endpoints: (builder) => ({
    createGroup: builder.mutation<IChannel, ICreateGroupCommand>({
      query: (body: ICreateGroupCommand) => ({
        url: '/channelsservice/channels/groups',
        method: 'POST',
        body,
      }),
    }),
  }),
  overrideExisting: false,
});

export const { useCreateGroupMutation } = groupsApi;
