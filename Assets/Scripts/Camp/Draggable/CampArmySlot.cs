using UnityEngine;

public class CampArmySlot : CampBasicSlot
{
    public int unlockPrice=15;

    public bool unlocked = false;
    public bool unlockable = false;
    public DebugText debugText;

    private void Awake()
    {
        debugText.ResetText();
    }
    public override bool IsSlotUnLocked()
    {
        return unlocked;
    }

    private void OnMouseUpAsButton()
    {
        if (unlocked)
        {
            if (IsSlotOccupied())
            {
                //Display UI Change mode
            }
            else
            {
                //Display UI Buy Unit
            }
            return;
        }


        if (unlockable)
        {
            //Try to Buy Slot
            //if bought
            unlocked = true;
            SetStateColor(null);
        }


        
    }
    private void OnMouseEnter()
    {
        if (unlocked)
        {
            if (IsSlotOccupied())
            {
                //Display text "Click to Change Unit Mode"
                debugText.SetText("Click to Change Unit Mode",Color.blue);
            }
            else 
            {
                //Display text "Click to buy Unit"
                debugText.SetText("Click to buy Unit", Color.blue);
            }
            return;
        }


        if (unlockable)
        {
            //Show buy cost
            debugText.SetText("Slot Unlock Price: " + unlockPrice, Color.yellow);
        }

        
    }
    private void OnMouseExit()
    {
        debugText.ResetText();
    }

    public bool IsUnlocked()
        { return unlocked; }
}
