using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerBag : MonoBehaviour
{
    [SerializeField] private int maxMoney;

    public int MaxMoney => maxMoney;

    [SerializeField] private int money;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI moneyAmount;
    public int Money => money;

    public UnityEvent OnMoneyChange;

    private void Start()
    {
        ChangeMoney(0);
        
    }

    public void ChangeMoney(int change)
    {
        
        money += change;
        if(money < 0)
            money = 0;

        float fillAmount = ((float)money / (float)maxMoney);
        if(fillAmount > 1)
            fillAmount = 1;
        image.fillAmount = fillAmount;
        moneyAmount.text = money.ToString();
        OnMoneyChange.Invoke();
    }




}
