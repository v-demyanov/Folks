import { useContext } from 'react';

import AuthContextValue from '../models/auth-context-value';
import AuthContext from '../context/auth-context';

const useAuth = (): AuthContextValue => {
  return useContext(AuthContext);
};

export default useAuth;
