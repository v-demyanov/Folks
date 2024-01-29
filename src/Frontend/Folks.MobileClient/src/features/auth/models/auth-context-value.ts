import { AuthRequest, TokenResponse } from 'expo-auth-session';

import SignInResult from './auth-result-type';
import ICurrentUser from './current-user';

interface IAuthContextValue {
  signInAsync: () => Promise<SignInResult>;
  signOutAsync: () => Promise<void>;
  authRequest: AuthRequest | null;
  currentUser: ICurrentUser | null;
  isAuthenticated: () => boolean;
  tokenResponse: TokenResponse | undefined;
}

export default IAuthContextValue;
