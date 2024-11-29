using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    [SerializeField] private int changeMoney = 10;

    private void OnTriggerEnter(Collider other)
    {
        PlayerBag bag = other.transform.parent.GetComponent<PlayerBag>();

        if (bag != null)
        {
            bag.ChangeMoney(changeMoney);
            Destroy(gameObject);
        }
    }

}
