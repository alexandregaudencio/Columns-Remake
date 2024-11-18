using System;


namespace ObjectPooling
{
    public interface IPoolable<T>
    {
        void Initialize(Action<T> returnAction);
        void ReturnToPool();

    }
}
