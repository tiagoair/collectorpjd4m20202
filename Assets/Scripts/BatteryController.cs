using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    #region Serializable Private Fields

    [SerializeField] private float timeToLive;
    
    #endregion

    #region Private Fields

    private float _createdTime;
    
    #endregion
    
    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    public void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimeAlive();
    }

    #endregion

    #region Private Fields

    private void CheckTimeAlive()
    {
        if (Time.time > _createdTime + timeToLive)
        {
            //Destroy(gameObject);
            BatteryPoolManager.Instance.DeactivateObject(gameObject);
        }
    }

    #endregion

    #region Public Methods

    public void ResetTimer()
    {
        _createdTime = Time.time;
    }

    #endregion
}
