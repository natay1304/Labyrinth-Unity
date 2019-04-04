using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionPreventor : MonoBehaviour
{
    [SerializeField]
    private Transform _avoidable;
    [SerializeField]
    private float _offset = 0.1f;
    private float _padding = 0.3f;
    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position - transform.up * _padding, _avoidable.up * _padding, out hit, _offset + _padding))
        {
            _avoidable.localPosition = (hit.distance - _offset - _padding) * Vector3.up;
        }
        else
        {
            _avoidable.localPosition = Vector3.zero;
        }
    }
}
