using UnityEngine;
using UnityEngine.Assertions;

public class PlayerNanobotFactory : MonoBehaviour, IEntityFactory
{
    private LevelDesignData levelDesignData = null;
    private GameObject currentNanobot = null;
    
    private Vector3 initialPosition = Vector3.zero;
    private bool wasInitialPositionSet = false;

    public void InitializeFactory(LevelDesignData _data)
    {
        Assert.IsNotNull(_data, "PlayerNanobotFactory's LevelDesignData is not assigned!");
        levelDesignData = _data;
        levelDesignData.OnNanobotDataChanged += ChangePlayerNanobot;
    }

    public void CreateEntity(Vector3 _position, Quaternion _rotation)
    {
        Assert.IsNotNull(levelDesignData, "PlayerNanobotFactory's levelDesignData is not assigned!");
        Assert.IsNotNull(levelDesignData.NanobotData, "PlayerNanobotFactory's levelDesignData's nanobotData is not assigned!");
        Assert.IsNotNull(levelDesignData.NanobotData.prefab, "PlayerNanobotFactory's NanobotData's prefab is not assigned!");

        if(!wasInitialPositionSet)
        {
            wasInitialPositionSet = true;
            initialPosition = _position;
        }

        if (currentNanobot != null)
        {
            Destroy(currentNanobot);
        }

        currentNanobot = Instantiate(levelDesignData.NanobotData.prefab, initialPosition, _rotation);

        PlayerNanobotController controller = null;
        TryGetComponent(out controller);
        if (controller == null)
        {
            controller = currentNanobot.AddComponent<PlayerNanobotController>();
        }

        Assert.IsNotNull(controller, "PlayerNanobotFactory's CreateEntity's controller is not assigned!");
        controller.Initialize(levelDesignData.NanobotData, levelDesignData.masterAbilityList, initialPosition);
    }

    private void OnEnable()
    {
        if (levelDesignData == null)
        {
            return;
        }

        levelDesignData.OnNanobotDataChanged += ChangePlayerNanobot;
    }

    private void OnDisable()
    {
        if (levelDesignData == null)
        {
            return;
        }

        levelDesignData.OnNanobotDataChanged -= ChangePlayerNanobot;
    }

    private void ChangePlayerNanobot(NanobotData _newNanobotData, NanobotData _oldNanobotData)
    {
        Vector3 position = currentNanobot != null ? currentNanobot.transform.position : Vector3.zero;
        Quaternion rotation = currentNanobot != null ? currentNanobot.transform.rotation : Quaternion.identity;
        CreateEntity(position, rotation);
    }
}