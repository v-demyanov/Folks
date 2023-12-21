import { createContext } from 'react';

import { IAuthContextValue, SignInResult } from '../models';

const defaultValue = {
  signInAsync: async (): Promise<SignInResult> => SignInResult.Error,
  signOutAsync: async (): Promise<void> => {},
  authRequest: null,
  currentUser: null,
  isAuthenticated: (): boolean => false,
} as IAuthContextValue;

const AuthContext = createContext<IAuthContextValue>(defaultValue);

export default AuthContext;
