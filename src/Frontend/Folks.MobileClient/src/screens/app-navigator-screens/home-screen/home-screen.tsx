import {
  CreateChannelButton,
  ChannelsList,
} from '../../../features/channels/components';
import HomeAppbar from '../../../features/home/home-appbar/home-appbar';

const HomeScreen = (): JSX.Element => {
  return (
    <>
      <HomeAppbar />
      <ChannelsList />
      <CreateChannelButton />
    </>
  );
};

export default HomeScreen;
