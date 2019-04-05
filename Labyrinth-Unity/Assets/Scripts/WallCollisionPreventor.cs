using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionPreventor : MonoBehaviour
{
    [SerializeField] private Transform _avoidable;
    [SerializeField] private float _avoidDistance = 0.3f;
    [SerializeField] private float _raycastOriginOffset = 0.5f;
    [SerializeField] private LayerMask _collisionMask;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            transform.position - transform.up * _raycastOriginOffset, _avoidable.up * _raycastOriginOffset, out hit, _avoidDistance + _raycastOriginOffset, _collisionMask.value))
        {
            _avoidable.localPosition = (hit.distance - _avoidDistance - _raycastOriginOffset) * Vector3.up;
        }
        else
        {
            _avoidable.localPosition = Vector3.zero;
        }
    }
}
