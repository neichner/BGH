using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NE.Core
{
    abstract public class ObjectPoolBehaviour : MonoBehaviour
    {
        public GameObject PooledObject;
        public int amountOfObjectsToPreload;
        public Queue<GameObject> gameObjects = new Queue<GameObject>();

        private void Awake()
        {
            for (int i = 0; i < amountOfObjectsToPreload; i++)
            {
                var instantiatedObject = Instantiate(PooledObject, transform);
                instantiatedObject.name = "Pooled Object " + i;
                instantiatedObject.SetActive(false);
                gameObjects.Enqueue(instantiatedObject);
            }
        }

        public GameObject GetPooledObject()
        {
            GameObject result;
            if (!gameObjects.TryDequeue(out result))
            {
                result = Instantiate(PooledObject, transform);
                result.SetActive(false);
            }
            return result;
        }

        public void ReturnObjectToPool(GameObject go)
        {
            go.transform.parent = transform;
            go.SetActive(false);
            gameObjects.Enqueue(go);
        }
    }
}