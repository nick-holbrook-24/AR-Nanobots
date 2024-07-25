using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceNanobot : MonoBehaviour
{
    private ARRaycastManager raycastManager = null;
    private ARPlaneManager planeManager = null;
    private IEntityFactory entityFactory = null;
    private bool isPlaced = false;

    public void Initialize(ARRaycastManager _raycastManager, ARPlaneManager _planeManager, IEntityFactory _entityFactory)
    {
        Assert.IsNotNull(_raycastManager, "ARPlaceNanobot's Initialize's parameter raycastManager is not assigned!");
        Assert.IsNotNull(_planeManager, "ARPlaceNanobot's Initialize's parameter planeManager is not assigned!");
        Assert.IsNotNull(_entityFactory, "ARPlaceNanobot's Initialize's parameter entityFactory is not assigned!");

        raycastManager = _raycastManager;
        planeManager = _planeManager;
        entityFactory = _entityFactory;

        isPlaced = false;
        enabled = true;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.V))
        {
            CreateEntity(Vector3.zero, Quaternion.identity);
            return;
        }
#endif
        if (isPlaced || Input.touchCount <= 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return;
        }

        Assert.IsNotNull(raycastManager, "ARPlaceNanobot's raycastManager is not assigned.");

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon);

        if (hits.Count <= 0)
        {
            return;
        }

        Pose hitPose = hits[0].pose;
        CreateEntity(hitPose.position, hitPose.rotation);
    }

    private void CreateEntity(Vector3 _position, Quaternion _rotation)
    {
        entityFactory.CreateEntity(_position, _rotation);
        isPlaced = true;

        planeManager.enabled = false;

        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}