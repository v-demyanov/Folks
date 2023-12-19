export {};

declare global {
  interface Array<T> {
    groupBy<TKey, TValue>(
      predicate: (value: TValue) => TKey
    ): Map<TKey, TValue[]>;
  }
}
