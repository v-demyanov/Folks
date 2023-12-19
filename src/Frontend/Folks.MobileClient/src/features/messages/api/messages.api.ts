import { HttpStatusCode } from 'axios';

import { api } from '../../../api/api';
import { channelsHubConnection } from '../../signalr/connections';
import ICreateMessageCommand from '../models/create-message-command';

const messagesApi = api.injectEndpoints({
  endpoints: (builder) => ({
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

export const { useSendMessageMutation } = messagesApi;
