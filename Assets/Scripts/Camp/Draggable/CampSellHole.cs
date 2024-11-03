using UnityEngine;

public class CampSellHole : MonoBehaviour, IDragListener, IDragPutTarget
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
        //TODO add math/balance to sell price
        int sellPrice = CalculateUnitSellPrice(unit);

        debugText.SetText($"Sell for {sellPrice} gold",Color.yellow);
    }

    public int CalculateUnitSellPrice(DraggableUnit unit)
    {
        return (int)(unit.GetUnitBlueprint().Config.price * unit.GetUnitBlueprint().Tier * 0.4f);
    }

    public bool PutUnit(DraggableUnit unit)
    {
        CampManager.Instance.AddMoney(CalculateUnitSellPrice(unit));
        CampManager.Instance.RemoveUnitFromCamp(unit.GetComponent<Unit>());
        return true;
    }
}
