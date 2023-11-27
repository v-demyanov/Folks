import HomeAppbar from '../../../features/home/home-appbar/home-appbar';
import HomeNavigator from '../../../navigation/home-navigator/home-navigator';

const HomeScreen = (): JSX.Element => {
  return (
    <>
      <HomeAppbar />
      <HomeNavigator />
    </>
  );
};

export default HomeScreen;
