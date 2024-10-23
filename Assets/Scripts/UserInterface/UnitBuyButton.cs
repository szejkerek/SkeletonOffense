using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitBuyButton : ViewPrefab<UnitDataUI>
{
    UnitConfig config;
    int unlockRound;
    public TMP_Text buttonText;
    public Button button;
    public static Action<UnitConfig, CampArmySlot> OnUnitBought;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(TryToBuyUnit);
    }
    public override void SetData(UnitDataUI data)
    {
        //TO DO check if unlocked based on round
        config = data.config;
        unlockRound = data.unlockRound;
        buttonText.text = $"{data.config.name} for {data.config.price}";
    }
    
    public void TryToBuyUnit()
    {
        if (CampManager.Instance.TryToBuy(config.price))
        {
            OnUnitBought?.Invoke(config, GameplayUI.Instance.campUnitManagmentUI.usedArmySlot);
        }
    }
}
