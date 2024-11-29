using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Character player;

    [SerializeField] private PlayerBag bag;
    [SerializeField] private Transform modelPlace;

    [SerializeField] private CharactersSequence sequence;
    [SerializeField] private GameObject currentModel;
    private int indexModel = 0;
    private Animator animator;
    private bool isGameEnd  = false;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private GameObject tutorialScreen;

    private bool isGamePaused = true;

    public void SetGameEnd(bool isGoodEnd)
    {
        isGameEnd = true;
        if (isGoodEnd)
        {
            animator.SetBool("goodDance", true);
           winPanel.SetActive(true);
        }
        else
        {
            animator.SetBool("badDance", true);
            losePanel.SetActive(true);
        }
    }

    float characterChangeMoney;
    private void Awake()
    {
        bag.OnMoneyChange.AddListener(ChangeModel);

    }
    private void Start()
    {
        animator = currentModel.GetComponent<Animator>();
        characterChangeMoney = (float)bag.MaxMoney / (float)sequence.characters.Length;
        
        
    }
    private int oldIndex;
    private void ChangeModel()
    {
           
        indexModel = (int)(bag.Money / characterChangeMoney);
        if (oldIndex == indexModel) return;
        if (currentModel != null)
            Destroy(currentModel);


        indexModel = Mathf.Clamp(indexModel, 0, sequence.characters.Length - 1);
        currentModel = Instantiate(sequence.characters[indexModel], new Vector3(0, 0, 0), Quaternion.Euler(modelPlace.transform.localEulerAngles), modelPlace);
        currentModel.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
        currentModel.GetComponent<Transform>().localRotation = Quaternion.Euler(0,0,0);
        animator = currentModel.GetComponent<Animator>();
        oldIndex = indexModel;
    }

    private void FixedUpdate()
    {
        if(isGameEnd || isGamePaused) { return; }
        player.MoveForward();
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tutorialScreen.SetActive(false);
            isGamePaused = false;
        }
        if (isGameEnd || isGamePaused) { return; }
        player.MoveSideToSide();

        
    }
}
