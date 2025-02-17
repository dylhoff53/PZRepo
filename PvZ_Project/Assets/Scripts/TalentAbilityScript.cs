using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentAbilityScript : MonoBehaviour
{
    public Talent talent;
    public bool offCooldown;
    public float cooldownTotalTime;
    public Slider cooldownSlider;
    public bool isTargetedAbility;
    public Transform targetNode;
    public bool hasSecondaryAttack;
    public Vector3 secondaryHalfExtents;
    public LayerMask nodeLayerMask;
    public Collider[] nodes;
    public bool isTowerSeller;
    public Color abilityColor;
    public Color secondaryColor;


    private void Update()
    {
        if(!offCooldown)
        {
            float percent = Time.deltaTime;
            cooldownSlider.value -= Mathf.InverseLerp(0, cooldownTotalTime, percent);
            if(cooldownSlider.value <= 0f)
            {
                offCooldown = true;
            }
        }
    }

    public virtual void UseAbility()
    {
        offCooldown = false;
        cooldownSlider.value = 1f;
        BuildManager.selectedAbility = null;
        BuildManager.instance.indicator.SetActive(false);
    }

    public void SelectAbility()
    {
        if (offCooldown)
        {
            Debug.Log("Rotationtest!!");
            BuildManager.turretToBuild = null;
            BuildManager.selectedAbility = this;
            BuildManager.instance.indicator.SetActive(true);
            BuildManager.instance.indicator.GetComponent<RectTransform>().position = new Vector3(gameObject.GetComponent<RectTransform>().position.x + 125f, gameObject.GetComponent<RectTransform>().position.y, 0f);
            BuildManager.instance.indicator.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }
}
