using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitConfig Config => config;
    [SerializeField] UnitConfig config;

    public UnitStateMachine UnitStateMachine => unitStateMachine;
    UnitStateMachine unitStateMachine;

    private void Awake()
    {
        unitStateMachine = GetComponent<UnitStateMachine>();
    }
}
