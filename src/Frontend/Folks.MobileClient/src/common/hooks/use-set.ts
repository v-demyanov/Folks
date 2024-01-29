import { useMemo, useRef, useState } from 'react';

export default function useSet<T>(initialValue: T[]): Set<T> {
  const triggerRender = useState(0)[1];
  const set = useRef<Set<T>>(new Set<T>(initialValue));

  return useMemo<Set<T>>(
    (): Set<T> =>
      ({
        add(item): void {
          if (set.current.has(item)) {
            return;
          }
          set.current.add(item);
          triggerRender((i) => ++i);
        },
        delete(item): void {
          if (!set.current.has(item)) {
            return;
          }
          set.current.delete(item);
          triggerRender((i) => ++i);
        },
        clear(): void {
          if (set.current.size === 0) {
            return;
          }
          set.current.clear();
          triggerRender((i) => ++i);
        },
        has: (item: T): boolean => set.current.has(item),
        keys: (): IterableIterator<T> => set.current.keys(),
        values: (): IterableIterator<T> => set.current.values(),
        forEach: (...args): void => set.current.forEach(...args),
        [Symbol.iterator]: (): IterableIterator<T> => set.current.values(),
        get size(): number {
          return set.current.size;
        },
      }) as Set<T>,
    [],
  );
}
