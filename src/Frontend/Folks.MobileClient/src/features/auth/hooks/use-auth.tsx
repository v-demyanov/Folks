import { createContext, useContext, useState } from 'react';
import * as WebBrowser from 'expo-web-browser';
import {
  AccessTokenRequestConfig,
  AuthRequestConfig,
  AuthSessionResult,
  TokenTypeHint,
  exchangeCodeAsync,
  fetchUserInfoAsync,
  makeRedirectUri,
  useAuthRequest,
  useAutoDiscovery,
} from 'expo-auth-session';
import * as SecureStore from 'expo-secure-store';

import IAuthProviderProps from '../models/auth-provider-props';
import AuthContextValue from '../models/auth-context-value';
import ICurrentUser from '../models/current-user';
import SignInResult from '../models/auth-result-type';

WebBrowser.maybeCompleteAuthSession();

const AuthContext = createContext<AuthContextValue>({
  signInAsync: async (): Promise<SignInResult> => SignInResult.Error,
  signOutAsync: async (): Promise<void> => {},
  authRequest: null,
  currentUser: null,
});

const redirectUri = makeRedirectUri();
const authRequestConfig: AuthRequestConfig = {
  clientId: 'native.code',
  redirectUri,
  scopes: ['openid', 'profile', 'chatServiceApi', 'email', 'phone'],
  responseType: 'code id_token',
  extraParams: {
    nonce: 'nonce',
  },
};

const AuthProvider = ({ children }: IAuthProviderProps) => {
  const [currentUser, setCurrentUser] = useState<ICurrentUser | null>(null);

  const discoveryDocument = useAutoDiscovery('http://192.168.0.104:8001');
  const [authRequest, authResult, promptAsync] = useAuthRequest(
    authRequestConfig,
    discoveryDocument
  );

  const signInAsync = async (): Promise<SignInResult> => {
    const authSessionResult = await promptAsync();

    switch (authSessionResult.type) {
      case SignInResult.Success:
        await handleSuccessAuthAsync(authSessionResult);
        return SignInResult.Success;
      default:
        return SignInResult.Error;
    }
  };

  const handleSuccessAuthAsync = async (
    authSessionResult: AuthSessionResult
  ): Promise<void> => {
    if (!discoveryDocument || authSessionResult.type !== SignInResult.Success) {
      return;
    }

    const accessTokenRequestConfig = {
      clientId: 'native.code',
      code: authSessionResult.params.code,
      redirectUri: redirectUri,
      extraParams: {
        code_verifier: authRequest?.codeVerifier || '',
      },
    } as AccessTokenRequestConfig;

    const tokenResponse = await exchangeCodeAsync(
      accessTokenRequestConfig,
      discoveryDocument
    );
    await SecureStore.setItemAsync(
      TokenTypeHint.AccessToken,
      tokenResponse.accessToken
    );

    const currentUser = (await fetchUserInfoAsync(
      tokenResponse,
      discoveryDocument
    )) as ICurrentUser;
    setCurrentUser(currentUser);
  };

  const signOutAsync = async (): Promise<void> => {
    if (!discoveryDocument || authResult?.type !== SignInResult.Success) {
      return;
    }

    await SecureStore.deleteItemAsync(TokenTypeHint.AccessToken);
    setCurrentUser(null);

    const urlParams = `id_token_hint=${authResult.params.id_token}&post_logout_redirect_uri=${redirectUri}`;
    const url = `${discoveryDocument.endSessionEndpoint}?${urlParams}`;
    authRequest?.promptAsync(discoveryDocument, { url });
  };

  return (
    <AuthContext.Provider
      value={{ signInAsync, signOutAsync, authRequest, currentUser }}
    >
      {children}
    </AuthContext.Provider>
  );
};

const useAuth = (): AuthContextValue => {
  return useContext(AuthContext);
};

export { AuthProvider };

export default useAuth;
