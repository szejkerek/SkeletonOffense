using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    IDamagable target;
    float speed;
    int damage;
    Vector3 lastDirection;
    
    public void Initialize(IDamagable target, int damage, float speed, bool follow = false)
    {
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        
        StartCoroutine(follow ? MoveToTarget() : MoveInDirection());
    }

    IEnumerator MoveInDirection()
    {
        if (target != null)
        {
            lastDirection = (target.transform.position - transform.position).normalized;
        }
        
        while (true)
        {
            transform.position += lastDirection * (speed * Time.deltaTime);

            if (target != null && target.IsAlive &&Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator MoveToTarget()
    {
        while (target != null && target.IsAlive)
        {
            lastDirection = (target.transform.position - transform.position).normalized;
            transform.position += lastDirection * (speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.3f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
                yield break;
            }
            yield return null;
        }
        
        while (true)
        {
            transform.position += lastDirection * (speed * Time.deltaTime);
            yield return null;
        }
    }
}