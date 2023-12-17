import { useContext } from 'react';

import IAuthContextValue from '../models/auth-context-value';
import AuthContext from '../context/auth-context';

const useAuth = (): IAuthContextValue => {
  return useContext(AuthContext);
};

export default useAuth;
