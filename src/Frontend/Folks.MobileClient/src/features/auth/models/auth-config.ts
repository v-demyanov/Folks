import { AuthRequestConfig } from 'expo-auth-session';

export default interface AuthConfig {
  authRequestConfig: AuthRequestConfig;
  identityServerUrl: string;
}
