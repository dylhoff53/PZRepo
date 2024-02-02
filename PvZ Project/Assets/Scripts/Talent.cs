using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent : Turret
{
    public TalentUI talentUI;
    public GameObject activeAbilityUIPrefab;
    public GameObject activeAbilityInstance;
    public Transform activeAbilityUIparent;

    public virtual void Start()
    {
        LeveledUp();
        activeAbilityInstance = Instantiate(activeAbilityUIPrefab);
        activeAbilityInstance.transform.SetParent(activeAbilityUIparent, false);
    }

    public override void Die()
    {
        talentUI.TalentDied();
        Destroy(activeAbilityInstance);
        base.Die();
    }


    public void LeveledUp()
    {
        if(talentUI.talentLevel == 1)
        {
            LeveledTo1();
        }else if (talentUI.talentLevel == 2)
        {
            LeveledTo2();
        }
        else if (talentUI.talentLevel == 3)
        {
            LeveledTo3();
        }
        else if (talentUI.talentLevel == 4)
        {
            LeveledTo4();
        }
        else if (talentUI.talentLevel == 5)
        {
            LeveledTo5();
        }
    }

    public virtual void LeveledTo1()
    {
        Debug.Log("Level 1 Thing!");
    }
    public virtual void LeveledTo2()
    {
        Debug.Log("Level 2 Thing!");
    }
    public virtual void LeveledTo3()
    {
        Debug.Log("Level 3 Thing!");
    }
    public virtual void LeveledTo4()
    {
        Debug.Log("Level 4 Thing!");
    }
    public virtual void LeveledTo5()
    {
        Debug.Log("Level 5 Thing!");
    }

}
