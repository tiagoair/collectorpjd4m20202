using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Serializable Private Fields

    [SerializeField] private GameObject objectToPool;

    [SerializeField] private int initialPoolSize;
    
    #endregion

    #region Private Fields

    private List<GameObject> _pooledObjects;

    #endregion
    
    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    protected void Start()
    {
        InitializePool();
    }

    #endregion

    #region Protected Fields

    protected void InitializePool()
    {
        _pooledObjects = new List<GameObject>();
        
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject createdObject = Instantiate(objectToPool, transform);
            createdObject.SetActive(false);
            _pooledObjects.Add(createdObject);
        }
    }

    #endregion

    #region Public Methods

    public virtual GameObject ActivateObject(Vector3 position)
    {
        if (_pooledObjects.Count > 0)
        {
            GameObject pooledObject = _pooledObjects[0];

            pooledObject.transform.position = position;
            pooledObject.SetActive(true);
            pooledObject.transform.parent = null;
            _pooledObjects.Remove(pooledObject);

            return pooledObject;
        }
        
        GameObject createdObject = Instantiate(objectToPool, position, Quaternion.identity);
        _pooledObjects.Add(createdObject);

        return createdObject;
    }

    public void DeactivateObject(GameObject pooledObject)
    {
        pooledObject.transform.parent = transform;
        pooledObject.SetActive(false);
        _pooledObjects.Add(pooledObject);
    }

    #endregion
}
