using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class BlindEffect : MonoBehaviour
{
    private PostProcessVolume _blinder;

    private void Start()
    {
        _blinder = GetComponent<PostProcessVolume>();
    }

    public void Blind(GameObject zone, Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(BlindCoroutine(4f));
    }

    private IEnumerator BlindCoroutine(float time)
    {
        _blinder.weight = 1;
        yield return new WaitForSeconds(time);
        _blinder.weight = 0;
    }
}