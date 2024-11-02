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
        return (int)(unit.unitBlueprint.Config.price * unit.unitBlueprint.Tier * 0.5f);
    }

    public bool PutUnit(DraggableUnit unit)
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
