using System;
using UnityEngine;

namespace ObjectPooling
{
    //Use as base class
    public class PoolObject : MonoBehaviour, IPoolable<PoolObject>
    {
        public Action<PoolObject> returnToPool;
        public void Initialize(Action<PoolObject> returnAction)
        {
            this.returnToPool = returnAction;
        }

        public void ReturnToPool()
        {
            returnToPool?.Invoke(this);
        }

        public virtual void OnDisable()
        {
            ReturnToPool();
        }



    }
}
