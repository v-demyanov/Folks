import { createContext } from 'react';

import AuthContextValue from '../models/auth-context-value';
import SignInResult from '../models/auth-result-type';

const defaultValue = {
  signInAsync: async (): Promise<SignInResult> => SignInResult.Error,
  signOutAsync: async (): Promise<void> => {},
  authRequest: null,
  currentUser: null,
} as AuthContextValue;

const AuthContext = createContext<AuthContextValue>(defaultValue);

export default AuthContext;
