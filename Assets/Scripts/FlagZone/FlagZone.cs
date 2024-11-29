using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FlagZone : MonoBehaviour
{
    [SerializeField] private Animator[] animators;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<Character>() != null)
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].enabled = true;
            }
        }
    }
}
