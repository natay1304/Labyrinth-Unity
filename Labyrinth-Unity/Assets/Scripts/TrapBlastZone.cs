using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapBlastZone : MonoBehaviour
{
    public UnityEvent TriggerEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent.Invoke();
    }
}
