import * as SecureStore from 'expo-secure-store';
import { useEffect, useState } from 'react';

const useSecureStore = <T extends unknown>(
  key: string,
  initialState: T | undefined = undefined
): [T | undefined, (value: T) => void, () => void] => {
  const [state, setState] = useState<T | undefined>(initialState);

  useEffect(() => {
    SecureStore.getItemAsync(key).then((value) => {
      if (value) {
        setState(JSON.parse(value));
      }
    });
  }, []);

  const clear = () => {
    SecureStore.deleteItemAsync(key);
    setState(undefined);
  };

  const setValue = (value: T) => {
    SecureStore.setItemAsync(key, JSON.stringify(value));
    setState(value);
  };

  return [state, setValue, clear];
};

export default useSecureStore;
