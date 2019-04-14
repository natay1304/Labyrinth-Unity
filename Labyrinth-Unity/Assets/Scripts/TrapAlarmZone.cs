using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TrapAlarmZoneEvent : UnityEvent<GameObject, Collider> {}

public class TrapAlarmZone : MonoBehaviour
{
    public TrapAlarmZoneEvent TriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent.Invoke(gameObject, other);
    }
}
