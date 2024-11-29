using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DoorType
{
    Poor,
    Common,
    Finish
}

[RequireComponent(typeof(BoxCollider))]
public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator[] doors;
    [SerializeField] private int moneyRequire;
    public int MoneyRequire => moneyRequire;
    [SerializeField] private PlayerBag bag;
    public PlayerBag Bag => bag;
    [SerializeField] private DoorType doorType;
    public DoorType DType => doorType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<Character>() != null)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if(moneyRequire <= bag.Money)
                    doors[i].enabled = true;
            }
        }
    }
}
