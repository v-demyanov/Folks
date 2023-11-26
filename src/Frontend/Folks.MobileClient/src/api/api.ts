import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { TokenResponse } from 'expo-auth-session';
import * as SecureStore from 'expo-secure-store';

import { TOKEN_RESPONSE_KEY } from '../common/constants/secure-store-keys.constants';

export const api = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: process.env.EXPO_PUBLIC_API_GTW_URL,
    prepareHeaders: async (headers) => {
      const rawTokenResponse = await SecureStore.getItemAsync(TOKEN_RESPONSE_KEY);

      if (rawTokenResponse) {
        const tokenResponse = JSON.parse(rawTokenResponse) as (TokenResponse | undefined);
        headers.set('authorization', `Bearer ${tokenResponse?.accessToken}`);
      }

      return headers;
    },
  }),
  endpoints: () => ({}),
});
