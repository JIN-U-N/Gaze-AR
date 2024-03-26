using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Linq;

public class BallSet : MonoBehaviour
{
    public ARRaycastManager aRRaycastManager; 
    public List<ARRaycastHit> hits= new List<ARRaycastHit>();
    List<InfoBehavior> infos= new List<InfoBehavior>();
    public bool IsObj = true;
    Transform CameraTransform;
    GameObject InfoObj;
    
    
    void Start()
    {
        infos = FindObjectsOfType<InfoBehavior>().ToList();
        CameraTransform=GameObject.Find("ARCamera").GetComponent<Transform>();
    }

    void Set()
    {
        if(Input.touchCount>0&&IsObj==true)
        {
            Vector2 touchPoint = Input.GetTouch(0).position;

            if(aRRaycastManager.Raycast(touchPoint,hits,TrackableType.PlaneWithinPolygon))
            {
                var hitPose=hits[0].pose;
                Instantiate(aRRaycastManager.raycastPrefab,hitPose.position,hitPose.rotation);
                IsObj=false;
            }
        }
    }

    void OpenInfo(InfoBehavior desiredInfo)
    {
        foreach(InfoBehavior info in infos)
        {
            if(info==desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
        }
    }

    void CloseAll()
    {
        foreach(InfoBehavior info in infos)
        {
            info.CloseInfo();
        }
    }
    
    void Gaze()
    {
        if (Physics.Raycast(CameraTransform.position,CameraTransform.forward,out RaycastHit hit))
        {
            InfoObj = hit.collider.gameObject;
            if (InfoObj != null)
            {
                OpenInfo(InfoObj.GetComponent<InfoBehavior>());
            }
            else
            {
                CloseAll();
            }
        }
    }
    
    void Update()
    {
        if(Input.touchCount>0&&IsObj==true)
        {
            Set();
        }
        Gaze();

    }
}

