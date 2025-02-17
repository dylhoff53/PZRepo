using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRamping : Enemy
{
    public int counter;
    public int[] damageList;

    public override void March()
    {
        base.March();
        counter = 0;
    }

    public override void Attack()
    {
        damage = damageList[counter];
        if(counter < 4)
        {
            counter++;
        }
        base.Attack();
    }
}
