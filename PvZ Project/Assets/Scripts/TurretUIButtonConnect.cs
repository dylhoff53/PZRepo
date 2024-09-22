using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUIButtonConnect : MonoBehaviour
{
    public Shop shop;
    public TurretBluePrint bp;
    // Start is called before the first frame update
    public void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    public void ButtonPressed()
    {

        shop.Selected(bp, this.GetComponent<RectTransform>().position.x);
    }

}
