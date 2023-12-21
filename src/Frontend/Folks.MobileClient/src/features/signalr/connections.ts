import * as SecureStore from 'expo-secure-store';
import { TokenResponse } from 'expo-auth-session';

import createHubConnection from './utils/create-hub-connection';
import { SecureStoreKeysConstants } from '../../common';

const channelsHubConnection = createHubConnection(
  `${process.env.EXPO_PUBLIC_API_GTW_URL}/channelsservice/hubs/channels`,
  {
    accessTokenFactory: accessTokenFactory,
  }
);

async function accessTokenFactory(): Promise<string> {
  const rawTokenResponse = await SecureStore.getItemAsync(
    SecureStoreKeysConstants.TOKEN_RESPONSE_KEY
  );
  if (rawTokenResponse) {
    const tokenResponse = JSON.parse(rawTokenResponse) as
      | TokenResponse
      | undefined;
    return tokenResponse?.accessToken ?? '';
  }

  return '';
}

export { channelsHubConnection };
