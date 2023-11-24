import HomeSearchbar from '../../../components/home/searchbar/home-searchbar';
import ChannelCreateButton from '../../../features/channels/channel-create-button/channel-create-button';
import ChannelsList from '../../../features/channels/channels-list/channels-list';

const ChannelsScreen = (): JSX.Element => {
  return (
    <>
      <ChannelsList />
      <ChannelCreateButton />
    </>
  );
};

export default ChannelsScreen;
