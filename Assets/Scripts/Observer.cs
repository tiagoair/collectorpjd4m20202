using System;

public static class Observer
{
    public static event Action<float> SetSlider;

    public static void OnSetSlider(float slide)
    {
        SetSlider?.Invoke(slide);
    }


    public static event Action EnableDeathText;

    public static void OnEnableDeathText()
    {
        EnableDeathText?.Invoke();
    }
}
