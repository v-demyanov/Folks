import { AuthRequest } from 'expo-auth-session';
import ICurrentUser from './current-user';
import SignInResult from './auth-result-type';

interface AuthContextValue {
  signInAsync: () => Promise<SignInResult>;
  authRequest: AuthRequest | null;
  currentUser: ICurrentUser | null;
}

export default AuthContextValue;
