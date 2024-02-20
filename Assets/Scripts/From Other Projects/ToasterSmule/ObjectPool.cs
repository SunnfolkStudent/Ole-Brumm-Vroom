using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace From_Other_Projects
{
    public interface IPooledObject
    {
        void OnObjectSpawn();
    }
    
    public class ObjectPool : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }
        
        public static ObjectPool Instance;

        private void Awake()
        {
            Instance = this;
        }
    
        public List<Pool> pools;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;
    
        [SerializeField] private float disappearTime = 2;

        private void Start()
        {
            StartCoroutine(DecreaseDisappearTime());
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
            
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                _poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(tag))
            {
                Debug.Log("Pool With tag"+ tag +" does not exist");
                return null;
            }
            GameObject objectToSpawn = _poolDictionary[tag].Dequeue();
        
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            StartCoroutine(deactivate()); 
        
            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }
        
            _poolDictionary[tag].Enqueue(objectToSpawn); 
        
            IEnumerator deactivate()
            {
                yield return new WaitForSeconds(disappearTime);
                objectToSpawn.SetActive(false);
            }
            return objectToSpawn;
        }

        private IEnumerator DecreaseDisappearTime()
        {
            while (true)
            {
                disappearTime /= 1.01f;
                yield return new WaitForSeconds(1);
            }
        }
    }
}