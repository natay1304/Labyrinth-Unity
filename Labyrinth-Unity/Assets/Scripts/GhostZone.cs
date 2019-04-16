using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostZone : MonoBehaviour
{
    [SerializeField] private Ghost _ghostPrefab;
    [SerializeField] private float _distanceToPlayer = 3;

    public void SpawnGhost(GameObject zone, Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Vector3 offset = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
            Vector3 ghostPosition = other.transform.position - _distanceToPlayer * offset;
            ghostPosition.y = this.transform.position.y;
            Instantiate(_ghostPrefab, ghostPosition, Quaternion.identity, this.transform);
        }
    }
}
