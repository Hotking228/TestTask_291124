using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }


    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
