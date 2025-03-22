using System.Collections.Generic;
using UnityEngine;

namespace dang
{
    public class ObjectPooling : MonoBehaviour
    {
        public GameObject objectPrefab;
        public int poolSize = 10;

        private Queue<GameObject> objectPool;

        void Awake()
        {
            objectPool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectPrefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
        }

        public GameObject GetObject()
        {
            if (objectPool.Count > 0)
            {
                GameObject obj = objectPool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject obj = Instantiate(objectPrefab);
                obj.SetActive(true);
                return obj;
            }
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }
}
