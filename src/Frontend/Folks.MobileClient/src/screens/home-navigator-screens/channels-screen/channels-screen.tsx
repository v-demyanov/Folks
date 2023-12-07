import CreateChannelButton from '../../../features/channels/components/create-channel-button/create-channel-button';
import ChannelsList from '../../../features/channels/components/channels-list/channels-list';

const ChannelsScreen = (): JSX.Element => {
  return (
    <>
      <ChannelsList />
      <CreateChannelButton />
    </>
  );
};

export default ChannelsScreen;
