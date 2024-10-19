using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Instance;

    public List<CampArmySlot> armyList = new List<CampArmySlot>();

    private int unlockableSlotsAmount = 2;
    private void Awake()
    {
        // Upewniamy siê, ¿e mamy tylko jedn¹ instancjê UnityDraggingManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {

        for (int i = 0;i< unlockableSlotsAmount;i++)
        {
            armyList[i].unlockable = true;
        }

        armyList[0].unlocked = true;
        
        foreach (var slot in armyList) 
        {
            //Load settings

            slot.SetStateColor(null);
        }
    }

    public void SetUnlocableToNextSlot()
    {
        if(armyList.Count > unlockableSlotsAmount)
        {
            armyList[unlockableSlotsAmount].unlockable = true;
            unlockableSlotsAmount++;
        }  
    }

    public void SpawArmy()
    {
        foreach (var slot in armyList)
        {
            if(slot.unlocked && slot.IsSlotOccupied())
            {
                //Use to spawn unit slot.unitOnSlot.unitBlueprint;
            }
        }
    }
}
