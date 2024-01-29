export default interface ICurrentUser {
  name: string;
  preferred_username: string;
  sub: string;
  email: string;
  email_verified: boolean;
  phone_number: string;
  phone_number_verified: boolean;
}
