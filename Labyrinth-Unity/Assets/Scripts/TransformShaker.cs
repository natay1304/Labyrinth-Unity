using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformShaker : MonoBehaviour
{   
    public void Shake(TrapAlarmZone trap)
    {
        Shake(trap.transform, 0.8f, 0.04f);
    }

    public void Shake(Transform toShake, float timeSpan = 1f, float shakeIntensivity = 0.1f)
    {
        StartCoroutine(ShakeCoroutines(toShake, timeSpan, shakeIntensivity));
    }
    
    private IEnumerator ShakeCoroutines(Transform toShake, float timeSpan, float shakeIntensivity)
    {
        Vector3 startPosition = toShake.position;
        float timer = timeSpan;
        Vector3 offset = Vector3.zero;
        while (timer > 0)
        {
            if (Random.value < 1f)
                offset = Random.insideUnitSphere * shakeIntensivity;
            toShake.position = Vector3.Lerp(toShake.position, startPosition + offset, 0.5f);
            timer -= Time.deltaTime;
            yield return null;
        }
        toShake.position = startPosition;
    }
}
