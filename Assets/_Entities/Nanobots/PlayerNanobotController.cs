using System;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerNanobotController : MonoBehaviour
{
    private MasterAbilityList masterAbilityList = null;
    private NanobotData nanobotData = null;
    private Rigidbody nanobotRigidbody = null;

    private Vector3 originPoint = Vector3.zero;

    private const byte defaultStatIncrease = 1;

    public void Initialize(NanobotData _data, MasterAbilityList _masterAbilityList, Vector3 _originPoint)
    {
        Assert.IsNotNull(_data, "PlayerNanobotController's Initialize's _data is not assigned!");
        nanobotData = _data;

        Assert.IsNotNull(_masterAbilityList, "PlayerNanobotController's Initialize's _masterAbilityList is not assigned!");
        masterAbilityList = _masterAbilityList;

        originPoint = _originPoint;

        MeshRenderer nanobotMeshRenderer = GetComponentInChildren<MeshRenderer>();
        Assert.IsNotNull(nanobotMeshRenderer, "PlayerNanobotController's meshRenderer is not assigned!");
        nanobotMeshRenderer.material = _data.material;

        nanobotRigidbody = GetComponent<Rigidbody>();
        Assert.IsNotNull(nanobotRigidbody, "PlayerNanobotController's nanobotRigidbody is not assigned!");

        gameObject.name = $"Player_Nanobot_{nanobotData.botName}";
        transform.localScale = nanobotData.scale;

        ActivateCurrentAbilities();
        MaxStats();
    }

    public void MaxStats()
    {
        nanobotData.energy.Current = nanobotData.energy.Max;
        nanobotData.cleanliness.Current = nanobotData.cleanliness.Max;
        nanobotData.DataChanged = true;
    }

    public void Recharge(uint _energyIncrease = defaultStatIncrease)
    {
        if (nanobotData.energy.Current >= nanobotData.energy.Max)
        {
            return;
        }

        nanobotData.energy.Current += _energyIncrease;
        nanobotData.DataChanged = true;
    }

    public void Clean(uint _cleanlinessIncrease = defaultStatIncrease)
    {
        if (nanobotData.cleanliness.Current >= nanobotData.cleanliness.Max)
        {
            return;
        }

        nanobotData.cleanliness.Current += _cleanlinessIncrease;
        nanobotData.DataChanged = true;
    }

    private void ActivateCurrentAbilities()
    {
        Assert.IsNotNull(masterAbilityList, "MasterAbilityList is not assigned!");
        for (int i = 0; i < nanobotData.abilities.Length; i++)
        {
            AbilityDefinition abilityDef = Array.Find(
                masterAbilityList.abilities, a => a.id == nanobotData.abilities[i].id);
            
            Assert.IsNotNull(abilityDef.abilityPrefab,
                "PlayerNanobotController's ActivateCurrentAbilities's abilityDef.abilityPrefab is not assigned!");
            if (abilityDef.abilityPrefab == null)
            {
                continue;
            }

            GameObject abilityInstance = Instantiate(abilityDef.abilityPrefab, transform);
            abilityInstance.name = abilityDef.abilityName;

            IAbility abilityComponent = abilityInstance.GetComponent<IAbility>();

            Assert.IsNotNull(abilityComponent,
                "PlayerNanobotController's ActivateCurrentAbilities's abilityComponent is not assigned!");
            if (abilityComponent == null)
            {
                continue;
            }
            
            nanobotData.abilities[i].abilityScript = abilityComponent;
            abilityComponent.Initialize(nanobotData.abilities[i].level, nanobotData);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < (originPoint.y + (transform.localScale.y * 0.5f)))
        {
            nanobotRigidbody.velocity = Vector3.zero;
            transform.position = originPoint + (Vector3.up * (transform.localScale.y * 0.5f));
            nanobotData.isJumping = false;
        }
    }
}