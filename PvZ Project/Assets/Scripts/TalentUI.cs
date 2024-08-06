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
        if(talentLevel != 0 && talentLevel < 5 && PlayerStats.Money >= levelPrices[price])
        {
            PlayerStats.Money -= levelPrices[price];
            talentLevel++;
            talent.LeveledUp();
            if(talentLevel != 5)
            {
                priceText.text = "$ " + levelPrices[price + 1].ToString();
            }
            else
            {
                priceText.text = "MAX";
                gameObject.GetComponent<Button>().interactable = false;
            }
        } else if (talentLevel == 0)
        {
            talentLevel++;
            everPlaced = true;
            priceText.text = "$ " + levelPrices[0].ToString();
        }
    }

    public void TalentDied()
    {
        priceText.text = "$ " + replacePrices[talentLevel - 1].ToString();
        talent.blueprint.cost = replacePrices[talentLevel - 1];
        upgradeImage.SetActive(false);
        gameObject.GetComponent<Button>().interactable = true;
    }
    
    public void Respawn()
    {
        priceText.text = "$ " + levelPrices[price + 1].ToString();
        upgradeImage.SetActive(true);
    }
}
