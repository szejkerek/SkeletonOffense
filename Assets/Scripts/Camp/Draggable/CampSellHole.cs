using UnityEngine;

public class CampSellHole : MonoBehaviour, IDragListener
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
        return (int)(unit.unitBlueprint.config.price * unit.unitBlueprint.level * 0.4f);
    }
}
