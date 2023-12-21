import { api } from '../../../api/api';
import { IUser } from '../models';

const usersApi = api.injectEndpoints({
  endpoints: (builder) => ({
    getAllUsers: builder.query<IUser[], null>({
      query: () => ({ url: '/identityservice/users', method: 'GET' }),
    }),
  }),
  overrideExisting: false,
});

export const { useGetAllUsersQuery } = usersApi;
