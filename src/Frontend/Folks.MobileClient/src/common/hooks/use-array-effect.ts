import { DependencyList, EffectCallback, useEffect, useRef } from 'react';
import { isEqual } from 'lodash';

export default function useArrayEffect(
  effect: EffectCallback,
  dependencies: DependencyList
): void {
  const ref = useRef<DependencyList>(dependencies);

  if (!isEqual(dependencies, ref.current)) {
    ref.current = dependencies;
  }

  useEffect(effect, [ref.current]);
}
