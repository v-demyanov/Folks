/* eslint-disable no-extend-native */
if (!Array.prototype.groupBy) {
  Array.prototype.groupBy = function <TKey, TValue>(
    predicate: (value: TValue) => TKey,
  ): Map<TKey, TValue[]> {
    return this.reduce((groups, item) => {
      const key = predicate(item);

      const groupItems = groups.get(key);
      if (groupItems) {
        groupItems.push(item);
      } else {
        groups.set(key, [item]);
      }

      return groups;
    }, new Map<TKey, TValue[]>());
  };
}
