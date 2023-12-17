import { AuthRequest, TokenResponse } from 'expo-auth-session';

import ICurrentUser from './current-user';
import SignInResult from './auth-result-type';

interface IAuthContextValue {
  signInAsync: () => Promise<SignInResult>;
  signOutAsync: () => Promise<void>;
  authRequest: AuthRequest | null;
  currentUser: ICurrentUser | null;
  isAuthenticated: () => boolean;
  tokenResponse: TokenResponse | undefined;
}

export default IAuthContextValue;
