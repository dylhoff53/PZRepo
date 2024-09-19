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

    }

    public void SelectAbility()
    {
        if (offCooldown)
        {
            BuildManager.turretToBuild = null;
            BuildManager.selectedAbility = this;
        }
    }
}
