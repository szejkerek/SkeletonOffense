using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitBuyButton : ViewPrefab<UnitDataUI>
{
    UnitDataUI data;
    public TMP_Text buttonText;
    public Button button;
    public static Action<UnitConfig, CampArmySlot,int> OnUnitBought;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(TryToBuyUnit);
    }
    public override void SetData(UnitDataUI data)
    {
        //TO DO check if unlocked based on round
        this.data = data;
        buttonText.text = $"{data.config.name} tier: {data.tier} for {data.config.price}";
    }
    
    public void TryToBuyUnit()
    {
        if (CampManager.Instance.TryToBuy(data.config.price))
        {
            OnUnitBought?.Invoke(data.config, GameplayUI.Instance.campUnitManagmentUI.usedArmySlot, data.tier);
            GetComponentInParent<NewUnitController>()?.RemoveItem(data);
        }
    }
}
