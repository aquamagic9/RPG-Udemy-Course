using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float hitDuration = 0.1f;
    private Material originalMat;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        Debug.Log(sr.material.name);
        originalMat = sr.material;
    }

    private IEnumerator FlashFX()
    {
        originalMat = sr.material;
        sr.material = hitMat;
        yield return new WaitForSeconds(hitDuration);
        sr.material = originalMat;
    }
}
