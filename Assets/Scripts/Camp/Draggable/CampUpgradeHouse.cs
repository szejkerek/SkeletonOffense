using UnityEngine;

public class CampUpgradeHouse : MonoBehaviour, IDragListener, IDragPutTarget
{

    public DebugText debugText;

    private void Start()
    {
        UnityDraggingManager.Instance.OnDragStart += OnDragStart;
        UnityDraggingManager.Instance.OnDragEnd += OnDragEnd;
        debugText.ResetText();
    }
    public void OnDragEnd(UnitDraggingManager unit)
    {
        debugText.ResetText();
    }

    public void OnDragStart(UnitDraggingManager unit)
    {
        //TODO add math/balance to upgrade price
        int upgradePrice = CalculateUnitUpgradePrice(unit);

        debugText.SetText($"Upgrade for {upgradePrice} gold", Color.yellow);
    }

    public int CalculateUnitUpgradePrice(UnitDraggingManager unit)
    {
        return (int)(unit.GetUnitBlueprint().Config.price * unit.GetUnitBlueprint().Tier * 0.5f);
    }

    public bool PutUnit(UnitDraggingManager unit)
    {
        unit.MoveToSlotPosition();
        
        // Unable to buy upgrade
        if (!CampManager.Instance.TryToBuy(CalculateUnitUpgradePrice(unit)))
            return false;

        //Upgrade unit
        unit.GetUnitBlueprint().TierUp();
        return true;
    }
}
