using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AshHeal : MonoBehaviour
{
    PjBase user;
    public GameObject wave;
    public float aArea;
    float healOverTime;
    float slowDuration;
    [HideInInspector]
    public List<PjBase> alliesAffected = new List<PjBase>();
    public void SetUp(PjBase user, float healOverTime, float slowDuration)
    {
        this.user = user;
        this.healOverTime = healOverTime;
        this.slowDuration = slowDuration;
    }
    private void Update()
    {
        CheckTargets();
    }
    public void CheckTargets()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, aArea * wave.transform.localScale.x, GameManager.Instance.playerLayer);
        PjBase enemy;
        foreach (Collider2D enemyColl in enemiesHit)
        {
            enemy = enemyColl.GetComponent<PjBase>();
            if (enemy.team == user.team && !alliesAffected.Contains(enemy))
            {
                if (enemy != user)
                {
                    enemy.AddComponent<AshRegen>().SetUp(user, healOverTime, slowDuration);
                    alliesAffected.Add(enemy);
                }
                else
                {
                    enemy.AddComponent<AshRegen>().SetUp(user, healOverTime/3, slowDuration);
                    alliesAffected.Add(enemy);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, aArea * wave.transform.localScale.x);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
