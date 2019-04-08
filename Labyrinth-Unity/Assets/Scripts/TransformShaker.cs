using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformShaker : MonoBehaviour
{
    public Transform _web;
    private void Start()
    {
        Shake(_web, 100f, 0.05f);
    }

    public void Shake(Transform toShake, float timeSpan = 1f, float shakeIntensivity = 0.1f, float deltaTime = 0.1f)
    {
        StartCoroutine(ShakeCoroutines(toShake, timeSpan, shakeIntensivity, deltaTime));
    }
    
    private IEnumerator ShakeCoroutines(Transform toShake, float timeSpan, float shakeIntensivity, float deltaTime)
    {
        float timer = timeSpan;
        Vector3 startPosition = toShake.position;
        while(timer > 0)
        {
            toShake.position = startPosition + Random.insideUnitSphere * shakeIntensivity;
            timer -= deltaTime;
            yield return new WaitForSeconds(deltaTime);
        }
        toShake.position = startPosition;
    }
}
