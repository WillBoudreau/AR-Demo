using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class InteractWithObject : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;//The raycast manager
    [SerializeField] private GameObject spawnedObject;//The object to spawn

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();//List of raycast hits

    void Update()
    {
        FindSpawnObject();
    }

    /// <summary>
    /// Find the spawn object in the scene
    /// </summary>
    void FindSpawnObject()
    {
        if(Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        if(touch.phase != TouchPhase.Began)
        {
            return;
        }

        if(raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;

            if(spawnedObject == null)
            {
                spawnedObject = Instantiate(spawnedObject, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}
