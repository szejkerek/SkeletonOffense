using UnityEngine;

public class CampSellHole : MonoBehaviour, IDragListener, IDragPutTarget
{

    public DebugText debugText;

    private void Start()
    {
        UnitDraggingManager.OnDragStart += OnDragStart;
        UnitDraggingManager.OnDragEnd += OnDragEnd;
        debugText.ResetText();
    }
    public void OnDragEnd(UnitDraggingManager unit)
    {
        debugText.ResetText();
    }

    public void OnDragStart(UnitDraggingManager unit)
    {
        //TODO add math/balance to sell price
        int sellPrice = CalculateUnitSellPrice(unit);

        debugText.SetText($"Sell for {sellPrice} gold",Color.yellow);
    }

    public int CalculateUnitSellPrice(UnitDraggingManager unit)
    {
        return (int)(unit.GetUnitBlueprint().Config.price * unit.GetUnitBlueprint().Tier * 0.4f);
    }

    public bool PutUnit(UnitDraggingManager unit)
    {
        CampManager.Instance.AddMoney(CalculateUnitSellPrice(unit));
        CampManager.Instance.RemoveUnitFromCamp(unit.GetComponent<Unit>());
        return true;
    }
    private void OnDestroy()
    {
        UnitDraggingManager.OnDragStart -= OnDragStart;
        UnitDraggingManager.OnDragEnd -= OnDragEnd;
    }
}
