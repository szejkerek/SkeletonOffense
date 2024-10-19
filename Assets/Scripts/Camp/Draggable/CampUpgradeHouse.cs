using UnityEngine;

public class CampUpgradeHouse : MonoBehaviour, IDragListener
{

    public DebugText debugText;

    private void Start()
    {
        UnityDraggingManager.Instance.OnDragStart += OnDragStart;
        UnityDraggingManager.Instance.OnDragEnd += OnDragEnd;
        debugText.ResetText();
    }
    public void OnDragEnd(DraggableUnit unit)
    {
        debugText.ResetText();
    }

    public void OnDragStart(DraggableUnit unit)
    {
        //TODO add math/balance to upgrade price
        int upgradePrice = CalculateUnitUpgradePrice(unit);

        debugText.SetText($"Upgrade for {upgradePrice} gold", Color.yellow);
    }

    public int CalculateUnitUpgradePrice(DraggableUnit unit)
    {
        return (int)(unit.unitBlueprint.price * unit.unitBlueprint.level * 0.5f);
    }

}
