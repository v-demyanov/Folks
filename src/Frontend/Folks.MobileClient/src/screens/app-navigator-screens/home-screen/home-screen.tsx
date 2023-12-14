import ChannelsList from '../../../features/channels/components/channels-list/channels-list';
import CreateChannelButton from '../../../features/channels/components/create-channel-button/create-channel-button';
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
