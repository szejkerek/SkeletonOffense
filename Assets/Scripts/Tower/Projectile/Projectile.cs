using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    IDamagable target;
    float speed;
    int damage = 0;
    public void Initialize(IDamagable target, int damage, float speed, bool follow = false)
    {
        this.damage = damage;
        this.target = target;
        this.speed = speed;
        StartCoroutine(follow ? MoveToTarget() : MoveInDirection());
    }

    IEnumerator MoveInDirection()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        while (target != null)
        {
            transform.position += direction * (speed * Time.deltaTime);

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

    IEnumerator MoveToTarget()
    {
        while (target != null && target.IsAlive)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * (speed * Time.deltaTime);

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
