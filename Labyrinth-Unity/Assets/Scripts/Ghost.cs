using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ghostSystem;

    private void Start()
    {
        StartCoroutine(GhostCoroutine(3f));
    }

    private void OnBecameVisible()
    {
        StartCoroutine(GhostCoroutine(0.1f));
    }

    private IEnumerator GhostCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _ghostSystem.Stop(true);
        Destroy(gameObject, 5);
    }
}
