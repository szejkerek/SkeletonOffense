using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "Scriptable Objects/TowerConfig")]
public class TowerConfig : ScriptableObject
{
    public int health;
    public float reloadTime;
    public Projectile projectile;
    public int damage;
    public float projectileSpeed;
    public float range;
}
