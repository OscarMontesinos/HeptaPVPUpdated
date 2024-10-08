using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshBasicExplosion : MonoBehaviour
{
    public float aArea;
    public void SetUp(PjBase user, float dmg)
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, aArea * transform.localScale.x, GameManager.Instance.playerLayer);
        PjBase enemy;
        foreach (Collider2D enemyColl in enemiesHit)
        {
            enemy = enemyColl.GetComponent<PjBase>();
            if (enemy.team != user.team)
            {
                enemy.GetComponent<TakeDamage>().TakeDamage(user, dmg, HitData.Element.ice, PjBase.AttackType.Magical);
                user.DamageDealed(user, enemy, dmg, HitData.Element.ice, HitData.AttackType.melee, HitData.HabType.basic);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, aArea*transform.localScale.x);
    }
}
