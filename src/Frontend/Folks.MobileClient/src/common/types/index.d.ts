export {};

declare global {
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  interface Array<T> {
    groupBy<TKey, TValue>(
      predicate: (value: TValue) => TKey,
    ): Map<TKey, TValue[]>;
  }
}
