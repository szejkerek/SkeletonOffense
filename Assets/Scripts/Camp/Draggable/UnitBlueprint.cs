using UnityEngine;

public class UnitBlueprint : MonoBehaviour
{
    public UnitConfig config;
    public int level = 1;
    public int price = 15;

    public void SpawnUnit()
    {
        Debug.Log("Unit" + config.name + "spawned");
    }

    public void LevelUp()
    {
        level++;
    }
}
