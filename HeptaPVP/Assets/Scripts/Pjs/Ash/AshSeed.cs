using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshSeed : Projectile
{
    public int seed;
    public GameObject blossomSeed;
    public GameObject blizzardSeed;
    public GameObject healSeed;
    float dmg;
    float slow;
    float duration;

    public void SetUp(PjBase user, float spd, float range, float dmg, float slow, float slowDuration, bool isAbility)
    {
        seed = 0;
        this.user = user;
        this.speed = spd;
        this.range = range;
        this.dmg = dmg;
        this.slow = slow;
        this.duration = slowDuration;

    }
    public void SetUp(PjBase user, float spd, float range, float dmg, float slow, float duration)
    {
        seed = 1;
        this.user = user;
        this.speed = spd;
        this.range = range;
        this.dmg = dmg;
        this.slow = slow;
        this.duration = duration;

    }
    public void SetUp(PjBase user, float spd, float range, float regen, float duration)
    {
        seed = 2;
        this.user = user;
        this.speed = spd;
        this.range = range;
        this.dmg = regen;
        this.duration = duration;

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.GetComponent<Barrier>() && collision.GetComponent<Barrier>().user.team != user.team && collision.GetComponent<Barrier>().damageable)
        {
            Die();
        }
    }

    public override void Die()
    {
        switch (seed)
        {
            case 0:
                AshBlossom blossom = Instantiate(blossomSeed, transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<AshBlossom>();
                blossom.SetUp(user,dmg,slow,duration);
                break;
            case 1:

                break;
            case 2:
                AshHeal heal = Instantiate(healSeed, transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<AshHeal>();
                heal.SetUp(user, dmg, duration);
                break;
        }
        base.Die();
    }
}
