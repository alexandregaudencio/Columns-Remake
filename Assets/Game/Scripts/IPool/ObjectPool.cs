using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    //Make static instance of this class on Object Spawner
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        public Action<T> pullObject;
        public Action<T> pushObject;
        private Stack<T> pooledObjects = new Stack<T>();
        private GameObject prefab;

        public ObjectPool(GameObject pooledObject)
        {
            prefab = pooledObject;
        }

        public ObjectPool(GameObject pooledObject, Action<T> pullObject, Action<T> pushObject)
        {
            prefab = pooledObject;
            this.pullObject = pullObject;
            this.pushObject = pushObject;

        }


        public int pooledCount
        {
            get
            {
                return pooledObjects.Count;
            }
        }

        public T Pull()
        {
            T t;
            if (pooledCount > 0)
                t = pooledObjects.Pop();
            else
                t = GameObject.Instantiate(prefab).GetComponent<T>();

            t.gameObject.SetActive(true); //ensure the object is on
            t.Initialize(Push);

            //allow default behavior and turning object back on
            pullObject?.Invoke(t);
            return t;
        }

        public T Pull(Vector3 position)
        {
            T t = Pull();
            t.transform.position = position;
            return t;
        }

        public T Pull(Vector3 position, Quaternion rotation)
        {
            T t = Pull();
            t.transform.position = position;
            t.transform.rotation = rotation;
            return t;
        }

        public GameObject PullGameObject()
        {
            return Pull().gameObject;
        }

        public GameObject PullGameObject(out T t)
        {
            t = Pull();
            return t.gameObject;
        }

        public GameObject PullGameObject(Vector3 position)
        {
            GameObject go = Pull().gameObject;
            go.transform.position = position;
            return go;
        }

        public GameObject PullGameObject(Vector3 position, out T t)
        {
            GameObject go = PullGameObject(out t);
            go.transform.position = position;
            return go;
        }

        public GameObject PullGameObject(Vector3 position, Quaternion rotation)
        {
            GameObject go = Pull().gameObject;
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }

        public GameObject PullGameObject(Quaternion rotation)
        {
            GameObject go = Pull().gameObject;
            go.transform.rotation = rotation;
            return go;
        }

        public GameObject PullGameObject(Quaternion rotation, out T t)
        {
            GameObject go = PullGameObject(out t);
            go.transform.rotation = rotation;
            return go;
        }

        public GameObject PullGameObject(Vector3 position, Quaternion rotation, out T t)
        {
            GameObject go = PullGameObject(out t);
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }

        public void Push(T t)
        {
            pooledObjects.Push(t);
            //create default behavior to turn off objects
            pushObject?.Invoke(t);

            t.gameObject.SetActive(false);
        }



    }
}