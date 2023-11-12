import { createContext, useContext, useState } from 'react';
import * as WebBrowser from 'expo-web-browser';
import {
  AccessTokenRequestConfig,
  AuthRequestConfig,
  AuthSessionResult,
  exchangeCodeAsync,
  fetchUserInfoAsync,
  makeRedirectUri,
  useAuthRequest,
  useAutoDiscovery,
} from 'expo-auth-session';
import * as SecureStore from 'expo-secure-store';

import IAuthProviderProps from '../models/auth-provider-props';
import AuthContextValue from '../models/auth-context-value';
import { ACCESS_TOKEN_KEY } from '../../../common/constants/secure-storage-keys.constants';
import ICurrentUser from '../models/current-user';
import SignInResult from '../models/auth-result-type';

WebBrowser.maybeCompleteAuthSession();

const AuthContext = createContext<AuthContextValue>({
  signInAsync: async (): Promise<SignInResult> => SignInResult.Error,
  authRequest: null,
  currentUser: null,
});

const redirectUri = makeRedirectUri();
const authRequestConfig: AuthRequestConfig = {
  clientId: 'native.code',
  redirectUri,
  scopes: ['openid', 'profile', 'chatServiceApi', 'email', 'phone'],
};

const AuthProvider = ({ children }: IAuthProviderProps) => {
  const [currentUser, setCurrentUser] = useState<ICurrentUser | null>(null);

  const discovery = useAutoDiscovery('http://192.168.0.104:8001');
  const [authRequest, , promptAsync] = useAuthRequest(
    authRequestConfig,
    discovery
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
    if (!discovery || authSessionResult.type !== SignInResult.Success) {
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
      discovery
    );
    await SecureStore.setItemAsync(ACCESS_TOKEN_KEY, tokenResponse.accessToken);

    const currentUser = (await fetchUserInfoAsync(
      tokenResponse,
      discovery
    )) as ICurrentUser;
    setCurrentUser(currentUser);
  };

  return (
    <AuthContext.Provider value={{ signInAsync, authRequest, currentUser }}>
      {children}
    </AuthContext.Provider>
  );
};

const useAuth = (): AuthContextValue => {
  return useContext(AuthContext);
};

export { AuthProvider };

export default useAuth;
