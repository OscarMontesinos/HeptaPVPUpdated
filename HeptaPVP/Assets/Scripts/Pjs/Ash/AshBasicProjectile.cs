using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshBasicProjectile : Projectile
{
    public GameObject explosion;
    public GameObject empoweredExplosion;
    float dmg;
    bool empower;
    public void SetUp(PjBase user, float spd, float range, float dmg)
    {
        this.user = user;
        this.speed = spd;
        this.range = range;
        this.dmg = dmg;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PjBase>() && collision.GetComponent<PjBase>().team != user.team)
        {
            if (collision.GetComponent<AshSlow>())
            {
                empower = true;
            }
            Die();
        }
        else if (collision.GetComponent<Barrier>() && collision.GetComponent<Barrier>().user.team != user.team && collision.GetComponent<Barrier>().damageable)
        {
            collision.GetComponent<TakeDamage>().TakeDamage(user, dmg, HitData.Element.ice, PjBase.AttackType.Magical);
            Die();
        }
        else if(collision.CompareTag("Wall") && collideWalls)
        {
            Die();
        }
        base.OnTriggerEnter2D(collision);
    }

    public override void Die()
    {
        if (empower)
        {
            AshBasicExplosion arrow = Instantiate(empoweredExplosion, transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<AshBasicExplosion>();
            arrow.SetUp(user, dmg);
        }
        else
        {
            AshBasicExplosion arrow = Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<AshBasicExplosion>();
            arrow.SetUp(user, dmg);
        }
        base.Die();
    }
}
