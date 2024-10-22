using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Instance;

    public List<CampArmySlot> armyList = new List<CampArmySlot>();

    public GameObject armySlots;

    private int unlockableSlotsAmount = 2;
    private void Awake()
    {
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

    public List<UnitBlueprint> GetArmy()
    {
        List<UnitBlueprint> tmpList = new List<UnitBlueprint>();
        foreach (var slot in armyList)
        {
            if(slot.unlocked && slot.IsSlotOccupied())
            {
                tmpList.Add(slot.GetUnitOnSlot().GetUnitBlueprint());
            }
        }
        return tmpList;
    }
}
