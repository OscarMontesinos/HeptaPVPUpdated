using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ash : PjBase
{
    Animator animator;
    public GameObject shooterPoint;
    public GameObject aBullet;
    public float aRange;
    public float aSpd;
    public float aDmg;

    int seedSelected;
    public float seedSpd;
    public GameObject seed;

    public float h1Range;
    public float h1Dmg;
    public float h1Slow;
    public float h1SlowDuration;

    public List<AshBlizzardFlower> h2FlowerList = new List<AshBlizzardFlower>();
    public bool h2InBlossoming;
    public float h2Pot;
    public float h2Range;
    public float h2Dmg;
    public float h2Slow;
    public float h2FlowerDuration;

    public float h3Range;
    public float h3HealOverTime;
    public float h3HealDuration;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void MainAttack()
    {
        base.MainAttack(); 
        if (!IsCasting() && !IsSoftCasting() && !IsStunned() && !IsDashing())
        {
            StartCoroutine(SoftCast(CalculateAtSpd(stats.atSpd)));
            animator.Play("AshBasic");
        }
    }
    
    public void ShootBasic()
    {
        AshBasicProjectile arrow = Instantiate(aBullet, shooterPoint.transform.position, pointer.transform.rotation).GetComponent<AshBasicProjectile>();
        arrow.SetUp(this, aSpd, aRange, CalculateSinergy(aDmg));
    }

    public override void Hab1()
    {
        base.Hab1();

        if (!IsCasting() && !IsStunned() && !IsDashing() && currentHab1Cd <= 0)
        {
            seedSelected = 0;
            animator.Play("AshSeed");
            StartCoroutine(SoftCast(CalculateAtSpd(2.5f)));
            currentHab1Cd = CDR(hab1Cd);
        }

    }
    public void ShootSeed()
    {
        switch (seedSelected)
        {
            case 0:
                SeedBlossom();
                break;

            case 1:
                SeedBlizzard();
                break;

            case 2:
                SeedHeal();
                break;
        }
    }

    public void SeedBlossom()
    {
        float range;
        Vector2 dist = transform.position - UtilsClass.GetMouseWorldPosition();
        Vector2 dist2 = transform.position - shooterPoint.transform.position;

        if (dist.magnitude - dist2.magnitude > h1Range)
        {
            range = h1Range;
        }
        else
        {

            range = dist.magnitude-dist2.magnitude;
        }

        AshSeed seed = Instantiate(this.seed, shooterPoint.transform.position, pointer.transform.rotation).GetComponent<AshSeed>();
        seed.SetUp(this, seedSpd, range, CalculateSinergy(h1Dmg),h1Slow,h1SlowDuration, true);
    }

    public override void Hab2()
    {
        base.Hab2(); 
        if (!IsCasting() && !IsStunned() && !IsDashing() && currentHab2Cd <= 0)
        {
            if (!h2InBlossoming)
            {
                h2InBlossoming = true;
                stats.pot += CalculateControl(h2Pot);
            }
            else
            {
                h2InBlossoming = true;
                stats.pot -= CalculateControl(h2Pot);
            }
            currentHab2Cd = 1;
        }
    }
    public void SeedBlizzard()
    {
        float range;
        Vector2 dist = transform.position - UtilsClass.GetMouseWorldPosition();
        Vector2 dist2 = transform.position - shooterPoint.transform.position;

        if (dist.magnitude - dist2.magnitude > h2Range)
        {
            range = h2Range;
        }
        else
        {

            range = dist.magnitude - dist2.magnitude;
        }

        AshSeed seed = Instantiate(this.seed, shooterPoint.transform.position, pointer.transform.rotation).GetComponent<AshSeed>();
        seed.SetUp(this, seedSpd, range, CalculateSinergy(h2Dmg),h2Slow,h2FlowerDuration);
    }

    public override void Hab3()
    {
        base.Hab3();
        if (!IsCasting() && !IsStunned() && !IsDashing() && currentHab3Cd <= 0)
        {
            seedSelected = 2;
            animator.Play("AshSeed");
            StartCoroutine(SoftCast(CalculateAtSpd(2.5f)));
            currentHab3Cd = CDR(hab3Cd);
        }
    }
    public void SeedHeal() 
    { 
        float range;
        Vector2 dist = transform.position - UtilsClass.GetMouseWorldPosition();
        Vector2 dist2 = transform.position - shooterPoint.transform.position;

        if (dist.magnitude - dist2.magnitude > h3Range)
        {
            range = h3Range;
        }
        else
        {

            range = dist.magnitude - dist2.magnitude;
        }

        AshSeed seed = Instantiate(this.seed, shooterPoint.transform.position, pointer.transform.rotation).GetComponent<AshSeed>();
        seed.SetUp(this, seedSpd, range, CalculateControl(h3HealOverTime),h3HealDuration);
    }
    
}
