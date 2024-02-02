using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalentUI : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public int[] levelPrices;
    public int[] replacePrices;
    public int talentLevel;
    public GameObject upgradeImage;
    public bool everPlaced;
    public int price;
    public Talent talent;


    public void UpgradeCheck(TurretBluePrint blueprint)
    {
        price = talentLevel - 1;
        if(talentLevel != 0 && PlayerStats.Money >= levelPrices[price])
        {
            PlayerStats.Money -= levelPrices[price];
            talentLevel++;
            talent.LeveledUp();
            priceText.text = "$ " + levelPrices[price + 1].ToString();
        } else if (talentLevel == 0)
        {
            talentLevel++;
            everPlaced = true;
            Debug.Log("Test!");
            priceText.text = "$ " + levelPrices[0].ToString();
        }
    }

    public void TalentDied()
    {
        priceText.text = "$ " + replacePrices[talentLevel - 1].ToString();
        talent.blueprint.cost = replacePrices[talentLevel - 1];
        upgradeImage.SetActive(false);
    }
    
    public void Respawn()
    {
        priceText.text = "$ " + levelPrices[price + 1].ToString();
        upgradeImage.SetActive(true);
    }
}
