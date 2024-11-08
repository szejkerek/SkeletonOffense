using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Instance;

    public List<CampArmySlot> armySlotsList = new List<CampArmySlot>();

    public List<CampBasicSlot> benchSlotsList = new List<CampBasicSlot>();

    public GameObject armySlots;

    public GameObject benchSlots;

    

    int unlockableSlotsAmount = 2;
    void Awake()
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
        benchSlotsList = benchSlots.GetComponentsInChildren<CampBasicSlot>().ToList();
        UnitBuyButton.OnUnitBought += SpawnUnitOnSlot;
    }

    public void SpawnUnitOnSlot(UnitConfig config, int tier = 1)
    {

        var modelPrefab = tier == 1 ? config.UnitModelTier1 : tier == 2 ? config.UnitModelTier2 : config.UnitModelTier3;
        var model = Instantiate(modelPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        if (!model.TryGetComponent(out Unit spawnedUnit))
        {
            Destroy(model);
            return;
        }

        CampBasicSlot slotToPlace = GetUnOccupiedSlot();
        spawnedUnit.PlaceInCamp(config, slotToPlace, tier);
        CampManager.Instance.AddUnitToCamp(spawnedUnit);


    }

    void Start()
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

    private CampBasicSlot GetUnOccupiedSlot()
    {
        foreach (var slot in armySlotsList)
        {
            if (slot.unlocked && !slot.IsSlotOccupied())
            {
                return slot;
            }
        }

        foreach (var slot in benchSlotsList)
        {
            if(!slot.IsSlotOccupied())
            {
                return slot;
            }
        }
        return null;
    }

    public bool IsFreeSlotToSpawn()
    {
        foreach (var slot in armySlotsList)
        {
            if (slot.unlocked && !slot.IsSlotOccupied())
            {
                return true;
            }
        }

        foreach (var slot in benchSlotsList)
        {
            if (!slot.IsSlotOccupied())
            {
                return true;
            }
        }
        return false;
    }
}
