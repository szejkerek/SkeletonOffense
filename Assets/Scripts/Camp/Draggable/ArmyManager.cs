using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Instance;

    public List<CampArmySlot> armyList = new List<CampArmySlot>();
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
}
