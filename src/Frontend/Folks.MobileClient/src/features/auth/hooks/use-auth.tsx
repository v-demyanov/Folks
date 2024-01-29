import { useContext } from 'react';

import AuthContext from '../context/auth-context';
import IAuthContextValue from '../models/auth-context-value';

const useAuth = (): IAuthContextValue => {
  return useContext(AuthContext);
};

export default useAuth;
