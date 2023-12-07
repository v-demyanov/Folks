import { api } from '../../../api/api';
import IChannel from '../../channels/models/channel';
import ICreateGroupCommand from '../models/create-group-command';

const groupsApi = api.injectEndpoints({
  endpoints: (builder) => ({
    createGroup: builder.mutation<IChannel, ICreateGroupCommand>({
      query: (body: ICreateGroupCommand) => ({
        url: '/chatservice/channels/groups',
        method: 'POST',
        body,
      }),
    }),
  }),
  overrideExisting: false,
});

export const { useCreateGroupMutation } = groupsApi;
