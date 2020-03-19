using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton
    private static ObjectPooler _instance;
    public static ObjectPooler Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    

    void Start()
    {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i<pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject pull(string tag)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }


        return poolDictionary[tag].Dequeue();
    }

    public void push(string tag, GameObject go)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");

        }
        else
        {
            poolDictionary[tag].Enqueue(go);
        }


        
    }



    public int countActives(string tag)
    {
        int count = 0;
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return 0;
        }
        IEnumerator<GameObject> enumerator = poolDictionary[tag].GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.activeSelf)
            {
                count++;
            }
        }
        return count;
    }

    public bool isFull(string tag)
    {
        int size = 0;
        foreach (Pool pool in pools)
        {
            if (string.Equals(pool.tag, tag))
            {
                size = pool.size;
            }
        }
        if (size == poolDictionary[tag].Count)
        {
            return true;
        }
        return false;
    }

}
