using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AshRegen : Buff
{
    float healOverTime;
    public void SetUp(PjBase user, float healOverTime, float duration)
    {
        this.user = user;
        this.healOverTime = healOverTime;
        time = duration;
        target = GetComponent<PjBase>();
    }
    public override void Update()
    {
        target.Heal(user,healOverTime * Time.deltaTime,HitData.Element.ice);
        base.Update();
    }

    public override void Die()
    {
        base.Die();
    }
}
