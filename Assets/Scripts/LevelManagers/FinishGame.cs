using ButchersGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    PlayerBag bag;
    DoorController doorController;
    LevelManager levelManager;


    

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        doorController = GetComponent<DoorController>();
        bag = doorController.Bag;
        
    }
    private bool isGoodEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<Character>() != null)
        {
            
            if (doorController.MoneyRequire > bag.Money)
            {
                other.transform.parent.GetComponent<Character>().ResetSpeed();
                if (doorController.DType == DoorType.Poor)
                {
                    
                    isGoodEnd = false;
                }
                if (doorController.DType == DoorType.Common)
                {
                    isGoodEnd = true;
                }
                if (doorController.DType == DoorType.Finish)
                {
                    isGoodEnd = true;
                }
                FindObjectOfType<CharacterController>().SetGameEnd(isGoodEnd);
            }
        }
       
        
    }




}
