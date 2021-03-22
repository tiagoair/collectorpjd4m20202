using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathText : MonoBehaviour
{
    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        Observer.EnableDeathText += EnableDeathText;
    }

    private void OnDisable()
    {
        Observer.EnableDeathText -= EnableDeathText;
    }

    #endregion

    #region Private Methods

    private void EnableDeathText()
    {
        GetComponent<TMP_Text>().enabled = true;
    }

    #endregion
}
