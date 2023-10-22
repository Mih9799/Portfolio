using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DirectorManager : MonoBehaviour
{
    [Header("Director System Manager V0.1.0")]
    [Header("Tick this variable to auto-play your sequence.")]
    [SerializeField] bool isDirectorActive = true;
    float shotDuration = 3.14f;
    bool autoAnimateCamera = false;
    bool rotateShot = false;
    Vector3 shotRotationSpeed;
    //[Range(5, 9)]
    bool useRandomDirection = true;
    float moveSpeed = 0.25f;
    float minMoveSpeed = -0.25f;
    float maxMoveSpeed = 0.25f;
   // [Header("Auto Detect Cameras")]
    //[Header("Enable this to auto-detect Cameras that are Parented to this Director Manager")]
    [Header("Auto Detect Cameras")]
    [Header("Enable this to auto-detect Cameras ", order = 0)]
    [Space(-10, order = 1)]
    [Header("that are Children of this GameObject", order = 2)]
    [Space(-10, order = 3)]
    [Header("  ", order = 4)]    
    [SerializeField] bool AutoFindCameras = false;
    [Space(10)]
    public List<Camera> ShotCameras;
    [Header("Shot Sequencing")]   
    Vector3 moveDirection; 
    Camera CurrentCamera;
    int shotIndex = 0;
    Vector3 shotStartLocation;
    //bool isDirectorActive = true;
    [Header("Ui Elements")]
    public GameObject DirectorCanvas;
    public GameObject DescriptionPanel;
    public Text titleText;
    public Text DescriptionText;
    bool isDescriptionActive = false;

    //public GameObject CamCollision;


    private void Awake()
    {
        if (AutoFindCameras || ShotCameras.Count < 0)
        {
            PopulateCameraList();
        }        
        //if (autoPopulatShotCameras)
        //{
        //    int idx = 0;
        //    foreach (Camera cam in ShotCameras)
        //    {
        //        ShotSequence[idx].thisCamera = cam;
        //    }
        //}
    }

    // Use this for initialization
    void Start()
    {
        disableAllCameras();
        moveDirection = Random.insideUnitSphere;
        CurrentCamera = ShotCameras[0];
        CurrentCamera.gameObject.SetActive(true);
        storeInitialCamLoc(CurrentCamera);
        DirectorCanvas.SetActive(true);
        DescriptionPanel.SetActive(isDescriptionActive);
        getInfoFromCamera();
        rotateShot = currentShotInfo.rotateCamera;
        shotRotationSpeed = currentShotInfo.rotationSpeed;
        //InvokeRepeating("SwapCamera", shotDuration, shotDuration);
        //StartStopDirector();
        if (isDirectorActive)
        {
            StartCoroutine(CountdownShot());
        }
        
    }
    public IEnumerator CountdownShot()
    {
        
        //Debug.Log("CoRot Started");
        yield return new WaitForSeconds(shotDuration);
        //Debug.Log("Corot Ended");
        SwapCamera();


    }
    // Update is called once per frame
    void Update()
    {
        if (autoAnimateCamera)
        {
            updateCameraMotion();
        }
    }

    public void PopulateCameraList()
    {
        ShotCameras.Clear();
        foreach (Camera tempChildCam in gameObject.GetComponentsInChildren<Camera>())
        {
            ShotCameras.Add(tempChildCam);
            //Debug.Log("Added " + tempChildCam.gameObject.name + "to shot camera list");
        }
        disableAllCameras();
    }

    public void disableAllCameras()
    {
        foreach (Camera cam in ShotCameras)
        {
            cam.gameObject.SetActive(false);
        }
    }

    public void SwapCamera()
    {
        //this will increment the camera to the next shot, we will call this at regular intervals
        //TODO - have a corresponding shot duration for each camera

        //Disable Current Camera
        CurrentCamera.gameObject.SetActive(false);
        CurrentCamera.gameObject.transform.position = shotStartLocation;

        if ((shotIndex + 1) < ShotCameras.Count)
            {
                shotIndex++;
            }
            else
            {
                shotIndex = 0;
            }

            //get the camera from the regular camera list
            CurrentCamera = ShotCameras[shotIndex];
            getInfoFromCamera();



        CurrentCamera.gameObject.SetActive(true);

        //CamCollision.transform.parent = CurrentCamera.transform;
        //CamCollision.transform.localPosition = Vector3.zero;
        
        storeInitialCamLoc(CurrentCamera);

        //handle drawing from shot info for speed
        if (!currentShotInfo)
        {
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        }
        else if (currentShotInfo)
        {
            moveSpeed = Random.Range(currentShotInfo.minShotSpeed, currentShotInfo.maxShotSpeed);
            shotDuration = currentShotInfo.shotDuration;
            autoAnimateCamera = currentShotInfo.animateCamera;
            rotateShot = currentShotInfo.rotateCamera;
            shotRotationSpeed = currentShotInfo.rotationSpeed;
        }
        

        if (useRandomDirection)
        {
            moveDirection = Random.insideUnitSphere;
        }
        if (currentShotInfo.useRandomRotation)
        {
            shotRotationSpeed = Random.insideUnitSphere;
        }
        else if (!useRandomDirection)
        {
            moveDirection = CurrentCamera.gameObject.transform.forward;
        }
        StopAllCoroutines();
        StartCoroutine(CountdownShot());
    }

    void updateCameraMotion()
    {
        if (autoAnimateCamera)
        {
            if (useRandomDirection)
            {
                CurrentCamera.gameObject.transform.position += (moveDirection * (moveSpeed * Time.deltaTime));
            }
            else
            {
                //CurrentCamera.gameObject.transform.Translate(CurrentCamera.gameObject.transform.forward * (moveSpeed * Time.deltaTime));
                CurrentCamera.gameObject.transform.position += (CurrentCamera.gameObject.transform.forward * (moveSpeed * Time.deltaTime));
            }

            if (rotateShot)
            {
                CurrentCamera.transform.Rotate(shotRotationSpeed * Time.deltaTime);
            }
            testCamCollision();
        }

    }
    void testCamCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(CurrentCamera.transform.position, 0.25f);
        if (hitColliders.Length > 0)
        {
            foreach (Collider col in hitColliders)
            {
                Debug.Log(col.gameObject.name);
            }
            //SwapCamera();
            autoAnimateCamera = false;
        }
    }
    void storeInitialCamLoc(Camera cam)
    {
        shotStartLocation = cam.transform.position;
    }

    public void StartStopDirector()
    {
        autoAnimateCamera = !autoAnimateCamera;
        isDirectorActive = !isDirectorActive;
        
        if (isDirectorActive)
        {
            StartCoroutine(CountdownShot());
            //InvokeRepeating("SwapCamera", shotDuration, shotDuration);
        }
        else if (!isDirectorActive)
        {
            //CancelInvoke();
            StopAllCoroutines();
        }
    }

    public void ShowHideDescription()
    {
        isDescriptionActive = !isDescriptionActive;
        DescriptionPanel.SetActive(isDescriptionActive);
    }

    DirectorCameraShotInfo currentShotInfo;

    public void getInfoFromCamera()
    {
        if (CurrentCamera.GetComponent<DirectorCameraShotInfo>())
        {
            currentShotInfo = CurrentCamera.GetComponent<DirectorCameraShotInfo>();
            titleText.text = currentShotInfo.shotName;
            DescriptionText.text = currentShotInfo.shotDescription;

        }
    }
}
