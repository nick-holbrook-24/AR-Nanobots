using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerNanobotView : MonoBehaviour
{
    [SerializeField] private LevelDesignData levelDesignData = null;

    [SerializeField] private TextMeshProUGUI nameText = null;
    [SerializeField] private TextMeshProUGUI energyText = null;
    [SerializeField] private TextMeshProUGUI cleanlinessText = null;
    [SerializeField] private AbilityButtonGenerator abilityButtonGenerator = null;
    [SerializeField] private NanobotSelectionGenerator nanobotSelectionGenerator = null;

    private void Start()
    {
        nanobotSelectionGenerator.SetDropdownActive(false);
    }

    private void OnEnable()
    {
        Assert.IsNotNull(levelDesignData, "PlayerNanobotView's levelDesignData is not assigned!");
        Assert.IsNotNull(levelDesignData.NanobotData, "PlayerNanobotView's levelDesignData's nanobotData is not assigned!");

        levelDesignData.NanobotData.OnDataChanged += UpdateView;
        levelDesignData.OnNanobotDataChanged += OnNanobotDataChanged;

    }

    private void OnDisable()
    {
        Assert.IsNotNull(levelDesignData, "PlayerNanobotView's levelDesignData is not assigned!");
        Assert.IsNotNull(levelDesignData.NanobotData, "PlayerNanobotView's levelDesignData's nanobotData is not assigned!");

        levelDesignData.NanobotData.OnDataChanged -= UpdateView;
        levelDesignData.OnNanobotDataChanged -= OnNanobotDataChanged;
    }

    private void UpdateView()
    {
        NanobotData data = levelDesignData.NanobotData;

        Assert.IsNotNull(data, "PlayerNanobotView's levelDesignData's nanobotData is not assigned!");
        Assert.IsNotNull(nameText, "PlayerNanobotView's nameText is not assigned!");
        Assert.IsNotNull(energyText, "PlayerNanobotView's energyText is not assigned!");
        Assert.IsNotNull(cleanlinessText, "PlayerNanobotView's cleanlinessText is not assigned!");

        nameText.text = $"Name: {data.botName}";
        energyText.text = $"Energy: {data.energy.Current}/{data.energy.Max}";
        cleanlinessText.text = $"Cleanliness: {data.cleanliness.Current}/{data.cleanliness.Max}";

        abilityButtonGenerator.CreateAbilityButtons(data, levelDesignData.masterAbilityList.abilities);

        bool isANanobotActive = !string.IsNullOrEmpty(data.botName);

        nanobotSelectionGenerator.SetDropdownActive(isANanobotActive);
    }

    private void OnNanobotDataChanged(NanobotData _newNanobotData, NanobotData _oldNanobotData)
    {
        _oldNanobotData.OnDataChanged -= UpdateView;
        _newNanobotData.OnDataChanged += UpdateView;

        UpdateView();
    }
}