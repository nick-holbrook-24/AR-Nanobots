using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class AbilityButtonGenerator : MonoBehaviour
{
    [SerializeField] private Button abilityButtonPrefab = null;
    [SerializeField] private Transform buttonContainer = null;

    public void CreateAbilityButtons(NanobotData nanobotData, AbilityDefinition[] abilityDataArray)
    {
        ClearExistingButtons();

        Assert.IsNotNull(abilityButtonPrefab, "AbilityButtonGenerator's abilityButtonPrefab is not assigned!");
        Assert.IsNotNull(buttonContainer, "AbilityButtonGenerator's buttonContainer is not assigned!");

        foreach (AbilityEquipped nanobotAbility in nanobotData.abilities)
        {
            AbilityDefinition abilityDefinition = FindAbilityDataById(nanobotAbility.id, abilityDataArray);

            if(string.IsNullOrEmpty(abilityDefinition.abilityName))
            {
                Debug.LogWarning($"CreateAbilityButtons's Nanobot Ability ID {nanobotAbility.id} was not found in the abilityDataArray.");
                continue;
            }

            Button newButton = Instantiate(abilityButtonPrefab, buttonContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityDefinition.abilityName;
            newButton.onClick.AddListener(() => nanobotAbility.abilityScript.Action());
        }
    }

    private void ClearExistingButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private AbilityDefinition FindAbilityDataById(byte id, AbilityDefinition[] abilityDataArray)
    {
        foreach (var abilityData in abilityDataArray)
        {
            if (abilityData.id == id)
            {
                return abilityData;
            }
        }

        return new AbilityDefinition();
    }
}
