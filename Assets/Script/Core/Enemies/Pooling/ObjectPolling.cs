using System.Collections.Generic;
using UnityEngine;

namespace dang
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int poolSize = 10;

        private List<GameObject> pool = new List<GameObject>();
        private GameObject poolContainer;

        void Awake()
        {
            pool = new List<GameObject>();
            poolContainer = new GameObject("Pool - " + enemyPrefab.name);

            CreatePool();
        }

        private void CreatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                pool.Add(CreateInstance());
            }
        }

        private GameObject CreateInstance()
        {
            GameObject instance = Instantiate(enemyPrefab);
            instance.transform.SetParent(poolContainer.transform);
            instance.SetActive(false);
            return instance;
        }

        public GameObject GetInstanceFromPool()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }

            return CreateInstance();
        }

        public static void ReturnToPool(GameObject instance)
        {
            if (instance != null)
            {
                instance.SetActive(false);
            }
        }
    }
}
