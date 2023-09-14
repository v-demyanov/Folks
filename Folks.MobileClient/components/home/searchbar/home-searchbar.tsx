import { useState } from 'react';
import { Searchbar } from 'react-native-paper';

const HomeSearchbar = (): JSX.Element => {
  const [searchQuery, setSearchQuery] = useState<string>('');

  const onChangeSearch = (query: string) => setSearchQuery(query);

  return (
    <Searchbar
      placeholder="Search"
      value={searchQuery}
      onChangeText={onChangeSearch}
      mode="view"
    />
  );
};

export default HomeSearchbar;
