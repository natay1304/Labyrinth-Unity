using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TrapAlarmZoneEvent : UnityEvent<TrapAlarmZone> {}

public class TrapAlarmZone : MonoBehaviour
{
    public TrapAlarmZoneEvent TriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent.Invoke(this);
    }
}
