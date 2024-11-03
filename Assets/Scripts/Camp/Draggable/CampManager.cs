using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CampManager : MonoBehaviour
{
    public static CampManager Instance;
    
    public CameraConfig CameraConfig => cameraConfig;
    [SerializeField] CameraConfig cameraConfig;  
    
    public event Action<int> OnMoneyChange;
    public int money;
    public List<Unit> inCampUnits = new List<Unit>();

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

    public bool TryToBuy(int price)
    {
        if(price <= money)
        {
            money -= price;
            OnMoneyChange.Invoke(money);
            return true;
        }
        return false;
    }

    public void AddMoney(int amount)
    { 
        money += amount;
        OnMoneyChange.Invoke(money);
    }

    public void AddUnitToCamp(Unit unit)
    {
        inCampUnits.Add(unit);
        CombineUnits();
    }

    public void RemoveUnitFromCamp(Unit unit, float delay = 0f)
    {
        inCampUnits.Remove(unit);
        unit.DestroyUnit(delay);
    }
    public void CombineUnits()
    {
        var groupedUnits = inCampUnits
            .GroupBy(unit => new { unit.Blueprint.Config, unit.Blueprint.Tier})
            .Where(group => group.Count() == 3)
            .ToList();

        if (groupedUnits.Any())
        {
            foreach (var unit in groupedUnits)
            {
                Debug.Log($"We have {unit.Key?.Config.name} to Combine");
                Debug.Log($"Unit 0 {unit.ElementAt(0)?.name} to Combine");

                UnitBlueprint unitToTierUP = unit.ElementAt(2).Blueprint;
                unitToTierUP.TierUp();


                RemoveUnitFromCamp(unit.ElementAt(0), 0.5f);
                RemoveUnitFromCamp(unit.ElementAt(1), 0.5f);
                RemoveUnitFromCamp(unit.ElementAt(2), 0.5f);

                ArmyManager.Instance.SpawnUnitOnSlot(unitToTierUP.Config, unitToTierUP.Tier);
            }
            
        }
    }

    public bool CheckIfWillCombine(UnitConfig unitConfig, int tier)
    {
        var groupedUnits = inCampUnits
            .GroupBy(unit => new { unit.Blueprint.Config, unit.Blueprint.Tier })
            .Where(group => group.Count() == 2 && group.Key.Config == unitConfig && group.Key.Tier == tier)
            .ToList();

        return groupedUnits.Any();
    }
}
