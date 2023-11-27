import ChannelCreateButton from '../../../features/channels/components/channel-create-button/channel-create-button';
import ChannelsList from '../../../features/channels/components/channels-list/channels-list';

const ChannelsScreen = (): JSX.Element => {
  return (
    <>
      <ChannelsList />
      <ChannelCreateButton />
    </>
  );
};

export default ChannelsScreen;
