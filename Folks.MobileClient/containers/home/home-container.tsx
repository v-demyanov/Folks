import HomeAppbar from '../../components/home/appbar/home-appbar';
import HomeNavigator from '../../navigation/home-navigator/home-navigator';

const HomeContainer = (): JSX.Element => {
  return (
    <>
      <HomeAppbar />
      <HomeNavigator />
    </>
  );
};

export default HomeContainer;
