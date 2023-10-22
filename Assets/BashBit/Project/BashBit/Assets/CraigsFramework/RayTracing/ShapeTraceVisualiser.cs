using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//References:
//https://docs.unity3d.com/ScriptReference/Physics.BoxCast.html

public class ShapeTraceVisualiser : MonoBehaviour {

    public bool showHitInfoUi = false;
    public bool isUIOpen = false;
    public Canvas uiCanvas;
    public Text ObjectName;
    public Text ObjectDistance;
    public Text ObjectHitPoint;
    public Text ObjectisRigidbody;

    public float traceDistance = 2.0f;

    public enum ShapeTypes {line, box, sphere, capsule }

    public ShapeTypes TraceType;
    public float traceRadius = 0.2f;
    RaycastHit traceHitInfo;
    // Use this for initialization
    void Start () {
        uiCanvas = GetComponentInChildren<Canvas>();
        uiCanvas.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {

        switch (TraceType)
        {
            case ShapeTypes.line:
                PerformLinetrace();
                break;
            case ShapeTypes.box:
                PerformBoxTrace();
                break;
            case ShapeTypes.sphere:
                PerformSphereTrace();
                break;
            case ShapeTypes.capsule:
                PerformCapsuleTrace();
                break;
        }
        if (traceHitInfo.collider)
        {
            if (!isUIOpen)
            {
                ShowHideHitInfo(true);
                isUIOpen = true;
            }
        }
        else
        {
            ShowHideHitInfo(false);
            isUIOpen = false;
        }
	}
    

    public void ShowHideHitInfo(bool show)
    {
        uiCanvas.gameObject.SetActive(show);
    }
    //this method will perform a Ray trace along this object's forward direction and return any hit found
    public void PerformLinetrace()
    {     
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward * traceDistance, out traceHitInfo, traceDistance))
        {
            //perform action based on trace, activate, grab, getcomponent, etc..
            
        }

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * traceDistance, Color.red, Time.deltaTime);
    }

    public void PerformBoxTrace()
    {
        

        if (Physics.BoxCast(transform.position + (transform.forward * (traceDistance / 2)), new Vector3(traceDistance, traceRadius, traceRadius), transform.forward, out traceHitInfo, transform.rotation))
        {
            
        }
    }

    public void PerformSphereTrace()
    {

    }

    public void PerformCapsuleTrace()
    {

    }

    private void OnDrawGizmos()
    {
        if (traceHitInfo.collider)
        {        
            switch (TraceType)
            {
                case ShapeTypes.line:
                    Gizmos.DrawSphere(traceHitInfo.point, traceRadius);
                    break;
                case ShapeTypes.box:
                    Gizmos.DrawCube(transform.position + (transform.forward * (traceDistance / 4)), new Vector3(traceRadius, traceRadius, traceDistance / 2));
                    break;
                case ShapeTypes.sphere:
                    //PerformSphereTrace();
                    break;
                case ShapeTypes.capsule:
                    //PerformCapsuleTrace();
                    break;
            }

            ObjectName.text = ("Name: " + traceHitInfo.collider.gameObject.name);
            ObjectDistance.text = ("Distance: " + traceHitInfo.distance.ToString());
            ObjectHitPoint.text = ("Hit Location" + traceHitInfo.point.ToString());

            if (traceHitInfo.collider.gameObject.GetComponent<Rigidbody>())
            {
                ObjectisRigidbody.text = "Object Simulating";
            }
            else
            {
                ObjectisRigidbody.text = "Object Static";
            }
            
        
        }
    }
}
