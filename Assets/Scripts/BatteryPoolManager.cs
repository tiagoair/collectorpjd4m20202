using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPoolManager : PoolManager
{
    #region Public Fields

    public static BatteryPoolManager Instance;

    #endregion
    
    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        
        //DontDestroyOnLoad(gameObject);
        
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    #endregion

    #region Public Methods

    public override GameObject ActivateObject(Vector3 position)
    {
        GameObject battery = base.ActivateObject(position);
        battery.GetComponent<BatteryController>().ResetTimer();
        return battery;
    }

    #endregion
}
