using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalentUI : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public int[] levelPrices;
    public int talentLevel;
    public GameObject upgradeImage;

    public void UpgradeCheck(TurretBluePrint blueprint)
    {
        int price = talentLevel - 1;
        if(talentLevel != 0 && PlayerStats.Money >= levelPrices[price])
        {
            PlayerStats.Money -= levelPrices[price];
            talentLevel++;
        } else if (talentLevel == 0)
        {
            talentLevel++;
        }
        priceText.text = "$" + levelPrices[price + 1].ToString();
    }
    

}
