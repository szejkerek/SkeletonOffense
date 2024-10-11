using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitConfig Config => config;
    [SerializeField] UnitConfig config;
}
