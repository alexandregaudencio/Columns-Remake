
namespace ObjectPooling
{
    public interface IPool<T>
    {
        /// <summary>
        /// release from the pool
        /// </summary>
        /// <returns></returns>
        T Pull();

        /// <summary>
        /// Return to pool.
        /// </summary>
        /// <param name="t"></param>
        void Push(T t);
    }

}

