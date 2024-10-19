using System;
using System.Collections.Generic;
using UnityEngine;

public class CampManager : MonoBehaviour
{
    public static CampManager Instance;
    public event Action<int> OnMoneyChange;
    public int money;
    private void Awake()
    {
        // Upewniamy siê, ¿e mamy tylko jedn¹ instancjê UnityDraggingManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool TryToBuy(int price)
    {
        if(price <= money)
        {
            money -= price;
            OnMoneyChange.Invoke(money);
            return true;
        }
        return false;
    }

    public void AddMoney(int amount)
    { 
        money += amount;
        OnMoneyChange.Invoke(money);
    }
}
