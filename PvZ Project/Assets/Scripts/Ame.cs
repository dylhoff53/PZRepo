using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ame : Talent
{
    public int maxAmmo;
    public int currentAmmoCount;
    public bool isReloading;
    public float reloadingTimer;
    public float totalReloadTime;
    public AmeTalentAbility ability;

    public override void Start()
    {
        base.Start();
        ability = FindObjectOfType<AmeTalentAbility>();
    }

    public override void Shoot()
    {
        base.Shoot();
        currentAmmoCount--;
        if(currentAmmoCount <= 0)
        {
            isReloading = true;
            fireCountdown = 0f;
        }
    }

    public override void Update()
    {
        if(!isReloading)
        {
            base.Update();
        }
        else
        {
            reloadingTimer += Time.deltaTime;
            if(reloadingTimer >= totalReloadTime)
            {
                isReloading = false;
                currentAmmoCount = maxAmmo;
                reloadingTimer = 0f;
            }
        }
    }

    public override void LeveledTo2()
    {
        base.LeveledTo2();
        maxHealth = 550;
        totalReloadTime = 4.5f;
        maxAmmo = 10;
    }

    public override void LeveledTo3()
    {
        base.LeveledTo3();
        maxHealth = 600;
        totalReloadTime = 4f;
        maxAmmo = 12;
        fireRate = 0.66f;
        blueprint.towerCooldown = 30f;
        ability.damage = 600;
        ability.secondaryDamage = 150;
        ability.cooldownTotalTime = 30f;
        ability.hasSecondaryAttack = true;
    }

    public override void LeveledTo4()
    {
        base.LeveledTo4();
        maxHealth = 650;
        totalReloadTime = 3.5f;
        maxAmmo = 14;
        fireRate = 0.66f;
        blueprint.towerCooldown = 30f;
        ability.damage = 600;
        ability.secondaryDamage = 150;
        ability.cooldownTotalTime = 30f;
        ability.hasSecondaryAttack = true;
    }

    public override void LeveledTo5()
    {
        base.LeveledTo5();
        maxHealth = 750;
        totalReloadTime = 3f;
        maxAmmo = 16;
        fireRate = 0.571f;
        blueprint.towerCooldown = 20f;
        ability.cooldownTotalTime = 25f;
        ability.damage = 650;
        ability.secondaryDamage = 350;
        ability.hasSecondaryAttack = true;
    }
}
