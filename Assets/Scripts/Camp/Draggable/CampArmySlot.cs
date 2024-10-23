using UnityEngine;

public class CampArmySlot : CampBasicSlot
{
    public int unlockPrice=15;

    public bool unlocked = false;
    public bool unlockable = false;
    public DebugText debugText;

    private void Start()
    {
        UnityDraggingManager.Instance.OnDragStart += OnDragStart;
        UnityDraggingManager.Instance.OnDragEnd += OnDragEnd;
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
                GameplayUI.Instance.campUnitManagmentUI.ShowEditUnitModeUI(this);
            }
            else
            {
                //Display UI Buy Unit
                GameplayUI.Instance.campUnitManagmentUI.ShowBuyUI(this);
            }
            return;
        }


        if (unlockable)
        {
            //Try to Buy Slot
            if (CampManager.Instance.TryToBuy(unlockPrice))
            {
                unlocked = true;
                ArmyManager.Instance.SetUnlocableToNextSlot();
                SetStateColor(null);
            }
                
            
        }


        
    }
    private void OnMouseEnter()
    {
        if (unlocked)
        {
            if (IsSlotOccupied())
            {
                //Display buttonText "Click to Change Unit Mode"
                debugText.SetText("Click to Change Unit Mode",Color.blue);
            }
            else 
            {
                //Display buttonText "Click to buy Unit"
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

}
