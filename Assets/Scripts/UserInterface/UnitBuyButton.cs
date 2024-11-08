using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitBuyButton : ViewPrefab<UnitDataUI>
{
    UnitDataUI data;
    public TMP_Text buttonText;
    public Button button;
    public Image bgImage;
    public static Action<UnitConfig, int> OnUnitBought;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(TryToBuyUnit);
    }
    public override void SetData(UnitDataUI data)
    {
        this.data = data;


        if (data is LootBoxDataUI lootBoxData)
        {
            buttonText.text = $"Loot Box for {lootBoxData.Price} Gold";
        }
        else
        {
            buttonText.text = $"{data.Config.name} Tier: {data.Tier} for {data.Config.price}";

            //Check if will combine
            if (CampManager.Instance.CheckIfWillCombine(data.Config, data.Tier))
            {
                buttonText.text += "But to TierUP";
            }
        }
    }
    
    public void TryToBuyUnit()
    {
        if (!ArmyManager.Instance.IsFreeSlotToSpawn() && !CampManager.Instance.CheckIfWillCombine(data.Config, data.Tier)) return;

        if (data is LootBoxDataUI lootBoxData)
        {
            if (CampManager.Instance.TryToBuy(lootBoxData.Price))
            {
                lootBoxData.OpenLootBox();
                GetComponentInParent<NewUnitController>()?.RemoveItem(data);
            }
        }
        else if (CampManager.Instance.TryToBuy(data.Config.price))
        {
            OnUnitBought?.Invoke(data.Config, data.Tier);
            GetComponentInParent<NewUnitController>()?.RemoveItem(data);
        }
    }
}
