using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AshBlossom : MonoBehaviour
{
    PjBase user;
    public GameObject particle;
    public float aArea;
    float dmg;
    float slow;
    float slowDuration;
    public void SetUp(PjBase user, float dmg, float slow, float slowDuration)
    {
        this.user = user;
        this.dmg = dmg;
        this.slow = slow;
        this.slowDuration = slowDuration;
    }
    public void Explode()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, aArea * transform.localScale.x, GameManager.Instance.playerLayer);
        PjBase enemy;
        foreach (Collider2D enemyColl in enemiesHit)
        {
            enemy = enemyColl.GetComponent<PjBase>();
            if (enemy.team != user.team)
            {
                enemy.GetComponent<TakeDamage>().TakeDamage(user, dmg, HitData.Element.ice, PjBase.AttackType.Magical);
                user.DamageDealed(user, enemy, dmg, HitData.Element.ice, HitData.AttackType.aoe, HitData.HabType.basic);
                enemy.AddComponent<AshSlow>().SetUp(user,slow,slowDuration);
            }
        }
        Instantiate(particle, transform.position, particle.transform.rotation);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, aArea * transform.localScale.x);
    }
}
