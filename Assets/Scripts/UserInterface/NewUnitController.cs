using UnityEngine;
using UnityEngine.UI;

public class NewUnitController : ListView<UnitDataUI>
{
    private UnitConfig[] unitConfigs;
    public Button rerollBtn;
    public int rerollPrice = 4;
    private void Start()
    {
        rerollBtn.onClick.AddListener(RerollShop);
        RebuildShop();
    }

    public void RemoveItem(UnitDataUI item)
    {
        Items.Remove(item);
    }
    
    private void RerollShop()
    {
        if(CampManager.Instance.TryToBuy(rerollPrice)) RebuildShop();
        //else TODO info not enought gold 
    }


    private void RebuildShop()
    {
        Items.Clear();
        unitConfigs = Resources.LoadAll<UnitConfig>("");
        if (unitConfigs.Length > 0)
        {

            var random = new System.Random();

            for (int i = 0; i < 3; i++)
            {
                int randomIndex = random.Next(unitConfigs.Length);
                UnitConfig selectedConfig = unitConfigs[randomIndex];

                int tier = random.NextDouble() < 0.7 ? 1 : 2;

                UnitDataUI unitData = new UnitDataUI(selectedConfig, tier);
                Items.Add(unitData);
            }
        }
    }

    private void OnDestroy()
    {
        rerollBtn.onClick.RemoveListener(RerollShop);
    }
}
