using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Utility class for holding camera shot information
public class DirectorCameraShotInfo : MonoBehaviour
{
    [Header("Director Camera Info")]
    [Header("This info is used when the ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Director switches to this Shot.", order = 2)]
    [Space(-10, order = 3)]
    [Header("Enter your info below.", order = 4)]
    [Space(10, order = 5)]
    public string shotName = "Shot Info Title";
    [TextArea] public string shotDescription = "This is the area for including descriptions, notes or other written information for the observer, this helps provide furhter context for the viewer. Your are limited to 290 characters.";
    [Space(10)]
    [Header("Shot Timing")]
    [Range(1.0f, 30.0f)] public float shotDuration = 3.14f;
    [Header("Animation and Behaviour Controls")]
    public bool animateCamera = true;
    [Space(10)]
    [Range(-1.0f, 1.0f)] public float minShotSpeed = -0.25f;
    [Range(-1.0f, 1.0f)] public float maxShotSpeed = 0.25f;
    [Space(10)]
    public bool rotateCamera = false;
    public bool useRandomRotation = false;
    public Vector3 rotationSpeed;    
}
