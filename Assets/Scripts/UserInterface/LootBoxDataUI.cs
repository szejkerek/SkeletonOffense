using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using System.Linq;

// Reprezentacja skrzynki w sklepie
public class LootBoxDataUI : UnitDataUI
{
    public int Price { get; private set; }

    public LootBoxDataUI(int price) : base(null, 0) 
    {
        this.Price = price;
    }


    public void OpenLootBox()
    {
        var random = new System.Random();
        double roll = random.NextDouble();

        if (roll < 0.5) // 50% for unit
        {
            int tier = random.NextDouble() < 0.5 ? 1 : (random.NextDouble() < 0.9 ? 2 : 3);
            List<UnitConfig> unitConfigs = Resources.LoadAll<UnitConfig>("").ToList();
            int randomIndex = random.Next(unitConfigs.Count);
            UnitConfig selectedConfig = unitConfigs[randomIndex];
            ArmyManager.Instance.SpawnUnitOnSlot(selectedConfig, tier);

            Debug.Log($"Some Unit {selectedConfig.name} with tier {tier}");
        }
        else if (roll < 0.9) // 40% for gold
        {
            int goldAmount = random.Next(10, 50);
            CampManager.Instance.AddMoney(goldAmount);
            Debug.Log($"Some gold {goldAmount}");
        }
        else 
        {
            Debug.Log("Other effect IDK");
        }

        //TODO VISUAL

    }
}
