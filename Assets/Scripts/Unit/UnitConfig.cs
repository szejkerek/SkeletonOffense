using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Scriptable Objects/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    [Header("Walk")]
    public float height = 1.5f;
    public float walkSpeed = 5f;
    [Header("Attack")]
    public float damage = 5f;
   [Header("Health")]
    public float health = 100f;
}
