import * as AuthSession from 'expo-auth-session';

import { AuthConfig } from '../models';

const redirectUri = AuthSession.makeRedirectUri();

const authConfig: AuthConfig = {
  authRequestConfig: {
    clientId: 'native.code',
    scopes: [
      'openid',
      'profile',
      'channelsServiceApi',
      'IdentityServerApi',
      'email',
      'phone',
    ],
    responseType: 'code id_token',
    extraParams: {
      nonce: 'nonce',
    },
    redirectUri,
  },
  identityServerUrl: process.env.EXPO_PUBLIC_IDENTITY_SERVER_URL ?? '',
};

export default authConfig;
