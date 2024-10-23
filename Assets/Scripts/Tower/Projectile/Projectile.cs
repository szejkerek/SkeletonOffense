using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    IDamagable target;
    int damage = 0;
    public void Initialize(IDamagable target, int damage)
    {
        this.damage = damage;
        this.target = target;
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while (target != null && target.IsAlive)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
