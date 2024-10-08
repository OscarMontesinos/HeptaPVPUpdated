using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AshBlizzardFlower : MonoBehaviour
{
    bool activated;
    PjBase user;
    float dmgOverTime;
    float slow;
    Animator animator;
    [HideInInspector]
    public List<PjBase> enemiesAffected = new List<PjBase>();

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetUp(PjBase user, float dmgOverTime, float slow)
    {
        this.user = user;
        this.dmgOverTime = dmgOverTime;
        this.slow = slow;
    }
    private void Update()
    {
        foreach (PjBase enemy in enemiesAffected)
        {
            enemy.GetComponent<TakeDamage>().TakeDamage(user, dmgOverTime, HitData.Element.ice, PjBase.AttackType.Magical);
            user.DamageDealed(user, enemy, dmgOverTime, HitData.Element.ice, HitData.AttackType.aoe, HitData.HabType.basic);
        }
    }

    public void Activate()
    {
        activated = true;
        animator.Play("FlowerChangeToWhite");
    }
    public void Deactivate()
    {
        activated = true;
        animator.Play("FlowerChangeToBlue");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PjBase>())
        {
            PjBase enemy = collision.GetComponent<PjBase>();
            enemy.stats.spd -= slow;
            enemiesAffected.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PjBase>())
        {
            PjBase enemy = collision.GetComponent<PjBase>();
            enemy.stats.spd += slow;
            enemiesAffected.Remove(enemy);
        }
    }

    void EndFlower()
    {
        animator.Play("FlowerDisappear");
    }

    public void Die()
    {
        foreach (PjBase enemy in enemiesAffected)
        {
            enemy.stats.spd += slow;
        }
        Destroy(gameObject);
    }
}
