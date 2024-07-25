using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.ARFoundation;

public class LevelDesignManager : MonoBehaviour
{
    [SerializeField] private LevelDesignData levelDesignData = null;
    [SerializeField] private MonoBehaviour entityFactory = null;

    private IEntityFactory playerNanobotFactory = null;

    void Start()
    {
        Assert.IsNotNull(levelDesignData, "FactoryManager's LevelDesignData is not assigned!");
        if (levelDesignData == null)
        {
            return;
        }

        playerNanobotFactory = entityFactory as IEntityFactory;
        Assert.IsNotNull(playerNanobotFactory, "FactoryManager's LevelDesignData is not assigned!");

        playerNanobotFactory.InitializeFactory(levelDesignData);

        ARPlaceNanobot arPlaceNanobot = new GameObject("AR_Place_Nanobot").AddComponent<ARPlaceNanobot>();
        arPlaceNanobot.Initialize(FindObjectOfType<ARRaycastManager>(), FindObjectOfType<ARPlaneManager>(), 
            playerNanobotFactory);
    }
}
