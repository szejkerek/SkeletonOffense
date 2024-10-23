using UnityEngine;

public class NewUnitController : ListView<UnitDataUI>
{
    private UnitConfig[] unitConfigs;
    private void Start()
    {
        Items.Clear();
        unitConfigs = Resources.LoadAll<UnitConfig>("");
        if (unitConfigs.Length > 0)
        {
            foreach (var unitConfig in unitConfigs)
            {
                Items.Add(new UnitDataUI(unitConfig));
            }
        }
    }
}
