using UnityEngine;

public class CampArmySlot : CampBasicSlot
{
    public int price;
    public bool unlocked = false;

    public override bool IsSlotUnLocked()
    {
        return unlocked;
    }

    private void OnMouseUpAsButton()
    {
        unlocked = true;
        SetStateColor(null);
    }
}
