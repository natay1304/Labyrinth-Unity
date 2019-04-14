using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TrapBlastZoneEvent : UnityEvent<GameObject, Collider> {}

public class TrapBlastZone : MonoBehaviour
{
    public TrapBlastZoneEvent TriggerEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent.Invoke(gameObject, other);
    }
}