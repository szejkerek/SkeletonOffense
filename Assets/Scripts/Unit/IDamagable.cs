using UnityEngine;

public interface IDamagable
{
    Transform transform { get; }
    GameObject gameObject { get; }
    public bool IsAlive {  get; set; }
    public void TakeDamage(int damage);
}