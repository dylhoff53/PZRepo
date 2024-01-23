using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MoneyFactory : MonoBehaviour
{
    public float maxTime;
    public float timer;
    public int moneyValue;
    public int healingAmount;
    public Transform tp;
    public string generationSound;
    public float generationSoundVolume;

    private void Start()
    {
        tp = BuildManager.turretParent.transform;
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= maxTime)
        {
            timer = 0f;
            Money();
            Heal();
        }
    }

    public void Money()
    {
        PlayerStats.Money += moneyValue;
        AudioManager.PlayOneShot(generationSound, generationSoundVolume, AudioManager.location);
    }

    public void Heal()
    {
        int count = tp.childCount;
        float currentLowest = 0f;
        int lowestIndex = 0;
        for(int i = 0; i < count; i++)
        {
            float health = tp.GetChild(i).GetChild(0).GetComponent<Turret>().health;
            float maxHealth = tp.GetChild(i).GetChild(0).GetComponent<Turret>().maxHealth;
            float percentage = health / maxHealth;

            if(currentLowest > percentage || i == 0)
            {
                currentLowest = percentage;
                lowestIndex = i;
            }
        }

        if(tp.GetChild(lowestIndex).GetChild(0).GetComponent<Turret>().maxHealth - tp.GetChild(lowestIndex).GetChild(0).GetComponent<Turret>().health < healingAmount)
        {
            tp.GetChild(lowestIndex).GetChild(0).GetComponent<Turret>().health = tp.GetChild(lowestIndex).GetChild(0).GetComponent<Turret>().maxHealth;
        } else
        {
            tp.GetChild(lowestIndex).GetChild(0).GetComponent<Turret>().health += healingAmount;
        }
    }
}
