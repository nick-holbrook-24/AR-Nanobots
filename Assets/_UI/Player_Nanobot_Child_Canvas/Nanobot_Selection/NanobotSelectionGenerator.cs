using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class NanobotSelectionGenerator : MonoBehaviour
{
    [SerializeField] private MasterNanobotList nanobotMasterList = null;
    [SerializeField] private TMP_Dropdown dropdown = null;
    [SerializeField] private LevelDesignData levelDesignData = null;

    public void SetDropdownActive(bool _isActive)
    {
        Assert.IsNotNull(dropdown, "NanobotSelectionGenerator's dropdown is not assigned!");
        dropdown.gameObject.SetActive(_isActive);
    }

    private void Start()
    {
        PopulateDropdown();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void PopulateDropdown()
    {
        Assert.IsNotNull(nanobotMasterList, "NanobotSelectionGenerator's nanobotMasterList is not assigned!");
        Assert.IsNotNull(dropdown, "NanobotSelectionGenerator's dropdown is not assigned!");

        dropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (NanobotData nanobotData in nanobotMasterList.nanobots)
        {
            options.Add(nanobotData.botName);
        }

        dropdown.AddOptions(options);
    }

    private void OnDropdownValueChanged(int index)
    {
        Assert.IsNotNull(levelDesignData, "NanobotSelectionGenerator's levelDesignData is not assigned!");

        NanobotData selectedNanobot = nanobotMasterList.nanobots[index];
        levelDesignData.NanobotData = selectedNanobot;
    }
}
