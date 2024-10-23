using System.Collections.Generic;
using UnityEngine;

public class CampUnitManagmentUI : MonoBehaviour
{
    List<UnitBuyButton> unitToBuyOptions;
    public GameObject newUnitContent;
    public GameObject editUnitModeContent;

    public CampArmySlot usedArmySlot;

    void Start()
    {
        //Spawn childs and add to list of units to buy based on avaliable configs
        HideUI();
    }

    public void ShowBuyUI(CampArmySlot campArmySlot)
    {
        HideUI();
        usedArmySlot = campArmySlot;
        newUnitContent.SetActive(true);
    }

    public void ShowEditUnitModeUI(CampArmySlot  campArmySlot)
    {
        HideUI();
        usedArmySlot = campArmySlot;
        editUnitModeContent.SetActive(true);
     // Set acording to button   campArmySlot.GetUnitOnSlot().GetUnitBlueprint().agressiveMode = true;
    }

    public void HideUI()
    {
        newUnitContent.SetActive(false);
        editUnitModeContent.SetActive(false);
    }
}
