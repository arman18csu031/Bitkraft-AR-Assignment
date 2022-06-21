using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ARController : MonoBehaviour
{
    public GameObject prefab;
    private GameObject spawnedObject;
    int count;
    public GameObject handamin;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }
        //Session.GetTrackables<DetectedPlane>(allPlanes, TrackableQueryFilter.All);

        Touch touch;
        if (Input.touchCount > 0 && ((touch = Input.GetTouch(0)).phase == TouchPhase.Began))
        {
            TrackableHit hit;
            TrackableHitFlags flags = TrackableHitFlags.PlaneWithinPolygon;
            if (Frame.Raycast(touch.position.x, touch.position.y, flags, out hit))
            {
                if ((hit.Trackable is DetectedPlane) && Vector3.Dot(Camera.main.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) > 0)
                {
                    DetectedPlane plane = hit.Trackable as DetectedPlane;
                    if (count < 1)
                    {
                        spawnedObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
                        spawnedObject.transform.Rotate(0, 180, 0);
                        count++;
                        handamin.SetActive(false);
                    }
                    /*else
                    {
                        spwanedObject.transform.position = hit.Pose.position;
                    }*/
                    var anchor = plane.CreateAnchor(hit.Pose);
                    spawnedObject.transform.parent = anchor.transform;
                }
            }
        }
    }
}
