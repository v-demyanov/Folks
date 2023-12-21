import { useEffect, useMemo, useState } from 'react';
import * as WebBrowser from 'expo-web-browser';
import * as AuthSession from 'expo-auth-session';

import {
  IAuthProviderProps,
  ICurrentUser,
  SignInResult,
  IAuthContextValue,
} from '../models';
import AuthContext from './auth-context';
import { useSecureStore } from '../../../common/hooks';
import { SecureStoreKeysConstants } from '../../../common';
import authConfig from '../configs/auth-config';

WebBrowser.maybeCompleteAuthSession();

const AuthProvider = ({ children }: IAuthProviderProps) => {
  const [currentUser, setCurrentUser] = useState<ICurrentUser | null>(null);

  const [tokenResponse, setTokenResponse, clearTokenResponse] = useSecureStore<
    AuthSession.TokenResponse | undefined
  >(SecureStoreKeysConstants.TOKEN_RESPONSE_KEY);

  const discoveryDocument = AuthSession.useAutoDiscovery(
    authConfig.identityServerUrl
  );

  const [authRequest, authResult, promptAsync] = AuthSession.useAuthRequest(
    authConfig.authRequestConfig,
    discoveryDocument
  );

  useEffect(() => {
    if (!tokenResponse || !discoveryDocument) {
      setCurrentUser(null);
      return;
    }

    AuthSession.fetchUserInfoAsync(tokenResponse, discoveryDocument).then(
      (currentUser) => {
        setCurrentUser(currentUser as ICurrentUser);
      }
    );
  }, [tokenResponse]);

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
    authSessionResult: AuthSession.AuthSessionResult
  ): Promise<void> => {
    if (discoveryDocument && authSessionResult.type === SignInResult.Success) {
      const accessTokenRequestConfig = {
        clientId: authConfig.authRequestConfig.clientId,
        code: authSessionResult.params.code,
        redirectUri: authConfig.authRequestConfig.redirectUri,
        extraParams: {
          code_verifier: authRequest?.codeVerifier || '',
        },
      } as AuthSession.AccessTokenRequestConfig;

      const tokenResponse = await AuthSession.exchangeCodeAsync(
        accessTokenRequestConfig,
        discoveryDocument
      );
      setTokenResponse(tokenResponse);
    }
  };

  const signOutAsync = async (): Promise<void> => {
    if (discoveryDocument && authResult?.type === SignInResult.Success) {
      clearTokenResponse();

      const idTokenHintParam = `id_token_hint=${authResult.params.id_token}`;
      const postLogoutRedirectUriParam = `post_logout_redirect_uri=${authConfig.authRequestConfig.redirectUri}`;
      const urlParams = `${idTokenHintParam}&${postLogoutRedirectUriParam}`;
      const url = `${discoveryDocument.endSessionEndpoint}?${urlParams}`;

      await authRequest?.promptAsync(discoveryDocument, { url });
    }
  };

  const isAuthenticated = (): boolean => currentUser !== null;

  const value = useMemo<IAuthContextValue>(
    () => ({
      signInAsync,
      signOutAsync,
      authRequest,
      currentUser,
      isAuthenticated,
      tokenResponse,
    }),
    [currentUser, authRequest, tokenResponse]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export { AuthProvider };
