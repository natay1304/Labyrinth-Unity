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

        if (HasItemDipping(out hit))
        {
            PullItem(GetItemDippingLevel(hit));
        }
        else
        {
            _avoidable.localPosition = Vector3.zero;
        }
    }

    private void PullItem(float dippingLevel)
    {
        _avoidable.localPosition = dippingLevel * Vector3.up;
    }

    private float GetItemDippingLevel(RaycastHit hit)
    {
        return (hit.distance - _avoidDistance - _raycastOriginOffset);
    }

    private bool HasItemDipping(out RaycastHit hit)
    {
        return Physics.Raycast(GetHandPivot(), GetHandDirection(), out hit, GetHandItemLength(), _collisionMask.value);
    }

    private Vector3 GetHandPivot()
    {
        return transform.position - transform.up * _raycastOriginOffset;
    }
    private Vector3 GetHandDirection()
    {
        return _avoidable.up * _raycastOriginOffset;
    }
    private float GetHandItemLength()
    {
        return _avoidDistance + _raycastOriginOffset;
    }
}