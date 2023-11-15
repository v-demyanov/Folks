import { useEffect, useState } from 'react';
import * as WebBrowser from 'expo-web-browser';
import * as AuthSession from 'expo-auth-session';

import IAuthProviderProps from '../models/auth-provider-props';
import ICurrentUser from '../models/current-user';
import SignInResult from '../models/auth-result-type';
import AuthContext from './auth-context';
import useSecureStore from '../../../common/hooks/use-secure-store';
import { TOKEN_RESPONSE_KEY } from '../../../common/constants/secure-store-keys.constants';
import authConfig from '../configs/auth-config';

WebBrowser.maybeCompleteAuthSession();
const redirectUri = AuthSession.makeRedirectUri();

const AuthProvider = ({ children }: IAuthProviderProps) => {
  const [currentUser, setCurrentUser] = useState<ICurrentUser | null>(null);

  const [tokenResponse, setTokenResponse, clearTokenResponse] = useSecureStore<
    AuthSession.TokenResponse | undefined
  >(TOKEN_RESPONSE_KEY);

  const discoveryDocument = AuthSession.useAutoDiscovery(
    authConfig.identityServerUrl
  );

  const [authRequest, authResult, promptAsync] = AuthSession.useAuthRequest(
    {
      ...authConfig.authRequestConfig,
      redirectUri,
    },
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
        redirectUri: redirectUri,
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

      const urlParams = `id_token_hint=${authResult.params.id_token}&post_logout_redirect_uri=${redirectUri}`;
      const url = `${discoveryDocument.endSessionEndpoint}?${urlParams}`;

      authRequest?.promptAsync(discoveryDocument, { url });
    }
  };

  return (
    <AuthContext.Provider
      value={{ signInAsync, signOutAsync, authRequest, currentUser }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export { AuthProvider };
