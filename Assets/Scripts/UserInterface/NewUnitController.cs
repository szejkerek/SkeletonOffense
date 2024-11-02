using UnityEngine;
using UnityEngine.UI;

public class NewUnitController : ListView<UnitDataUI>
{
    private UnitConfig[] unitConfigs;
    public Button rerollBtn;
    private void Start()
    {
        rerollBtn.onClick.AddListener(RebuildShop);
        RebuildShop();
    }

    public void RemoveItem(UnitDataUI item)
    {
        Items.Remove(item);
    }

    private void RebuildShop()
    {
        Items.Clear();
        unitConfigs = Resources.LoadAll<UnitConfig>("");
        if (unitConfigs.Length > 0)
        {

            var random = new System.Random();

            // Losowo wybieramy trzy konfiguracje (mog¹ siê powtarzaæ)
            for (int i = 0; i < 3; i++)
            {
                // Losowanie konfiguracji
                int randomIndex = random.Next(unitConfigs.Length);
                UnitConfig selectedConfig = unitConfigs[randomIndex];

                // Losowanie tieru z prawdopodobieñstwem 70% dla 1 i 30% dla 2
                int tier = random.NextDouble() < 0.7 ? 1 : 2;

                // Tworzymy UnitDataUI z wylosowan¹ konfiguracj¹ i tierem
                UnitDataUI unitData = new UnitDataUI(selectedConfig, tier);
                Items.Add(unitData);
            }
        }
    }

    private void OnDestroy()
    {
        rerollBtn.onClick.RemoveListener(RebuildShop);
    }
}
