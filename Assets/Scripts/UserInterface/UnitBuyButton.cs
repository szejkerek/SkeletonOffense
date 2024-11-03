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
        buttonText.text = $"{data.config.name} tier: {data.tier} for {data.config.price}";

        //Check if will combine
        if(CampManager.Instance.CheckIfWillCombine(data.config, data.tier))
        {
            buttonText.text += "But to TierUP";
        }
    }
    
    public void TryToBuyUnit()
    {
        if (CampManager.Instance.TryToBuy(data.config.price))
        {
            OnUnitBought?.Invoke(data.config, data.tier);
            GetComponentInParent<NewUnitController>()?.RemoveItem(data);
        }
    }
}
