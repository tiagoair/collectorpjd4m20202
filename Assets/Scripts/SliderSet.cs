using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSet : MonoBehaviour
{
    #region Private Fields

    private Slider _slider;
        
    #endregion
    
    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        Observer.SetSlider += SetSlider;
    }

    private void OnDisable()
    {
        Observer.SetSlider -= SetSlider;
    }

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    #endregion

    #region Private Methods

    private void SetSlider(float slide)
    {
        _slider.value = slide;
    }

    #endregion
}
