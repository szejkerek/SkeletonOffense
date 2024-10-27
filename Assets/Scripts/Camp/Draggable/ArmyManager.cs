using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Instance;

    public List<CampArmySlot> armySlotsList = new List<CampArmySlot>();

    public GameObject armySlots;

    public GameObject unitPrefab;

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

        armySlotsList = armySlots.GetComponentsInChildren<CampArmySlot>().ToList();
        UnitBuyButton.OnUnitBought += SpawnUnitOnSlot;
    }

    private void SpawnUnitOnSlot(UnitConfig config, CampArmySlot slot)
    {
        
        GameObject newUnit = Instantiate(unitPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(config.UnitModel, new Vector3(0, 0, 0), Quaternion.identity,newUnit.transform);
        DraggableUnit unitDraggable = newUnit.GetComponent<DraggableUnit>();
        unitDraggable.GetUnitBlueprint().config = config;
        unitDraggable.GetUnitBlueprint().level = 1;
        unitDraggable.SetCurrentSlot(slot);
        unitDraggable.MoveToSlotPosition();
    }

    private void Start()
    {
        
        for (int i = 0;i< unlockableSlotsAmount;i++)
        {
            armySlotsList[i].unlockable = true;
        }

        armySlotsList[0].unlocked = true;
        
        foreach (var slot in armySlotsList) 
        {
            slot.SetStateColor(null);
        }
    }

    public void SetUnlocableToNextSlot()
    {
        if(armySlotsList.Count > unlockableSlotsAmount)
        {
            armySlotsList[unlockableSlotsAmount].unlockable = true;
            unlockableSlotsAmount++;
        }  
    }

    public List<UnitBlueprint> GetArmy()
    {
        List<UnitBlueprint> tmpList = new List<UnitBlueprint>();
        foreach (var slot in armySlotsList)
        {
            if(slot.unlocked && slot.IsSlotOccupied())
            {
                tmpList.Add(slot.GetUnitOnSlot().GetUnitBlueprint());
            }
        }
        return tmpList;
    }
    private void OnDestroy()
    {
        UnitBuyButton.OnUnitBought -= SpawnUnitOnSlot;
    }
}
