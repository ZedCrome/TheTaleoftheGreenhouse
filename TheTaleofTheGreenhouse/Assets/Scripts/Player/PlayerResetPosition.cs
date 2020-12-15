using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetPosition : MonoBehaviour
{
    private void OnEnable()
    {
        DayNightCycle.instance.onSleep += OnSleep;
    }
    
    private void OnDisable()
    {
        DayNightCycle.instance.onSleep -= OnSleep;
    }
    
    void OnSleep()
    {
        transform.localPosition = new Vector3(-0.6579178f, 0.8477007f, 0.0f);
    }
}
