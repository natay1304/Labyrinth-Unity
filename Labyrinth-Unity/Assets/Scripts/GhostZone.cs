using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostZone : MonoBehaviour
{
    [SerializeField] private Ghost _ghostPrefab;
    [SerializeField] private LayerMask _wallMask;
    [SerializeField] private float _maxDistanceToGhost;
    

    public void SpawnGhost(GameObject zone, Collider other)
    {
        if (other.CompareTag("Player")) 
        {        
            Instantiate(_ghostPrefab, GetSpawnPosition(Camera.main.transform), Quaternion.identity, this.transform);
        }
    }

    private Vector3 GetSpawnPosition(Transform playerHead)
    {
        RaycastHit hit;
        Vector3 offset = Vector3.ProjectOnPlane(playerHead.forward, Vector3.up).normalized;

        Vector3 spawnPosition;
        if(Physics.Raycast(Camera.main.transform.position, offset, out hit, _maxDistanceToGhost, _wallMask))
        {
            spawnPosition = hit.point - offset * 0.2f;
        }
        else
        {
            spawnPosition = playerHead.position - _maxDistanceToGhost * offset;
        }         
        
        spawnPosition.y = this.transform.position.y;
        return spawnPosition;
    }
}
