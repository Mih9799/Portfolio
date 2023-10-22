using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour {

    public float MinDarkTime = 0.05f;
    public float MaxDarkTime = 0.25f;

    public float MinLightTime = 0.05f;
    public float MaxLightTime = 0.5f;

    bool isSwitchedOn = false;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Light>().intensity = 0;
    }
    public void toggleLight()
    {
        if (isSwitchedOn == false)
        {
            StartCoroutine(FlickerLight());
            isSwitchedOn = true;
        }
        else if(isSwitchedOn == true)
        {
            //StopCoroutine(FlickerLight());
            StopAllCoroutines();
            gameObject.GetComponent<Light>().intensity = 0;
            isSwitchedOn = false;

        }

    }

    IEnumerator FlickerLight()
    {
        while (true)
        {
            gameObject.GetComponent<Light>().intensity = 0;
            yield return new WaitForSeconds(Random.Range(MinDarkTime, MaxDarkTime));
            gameObject.GetComponent<Light>().intensity = 1;
            yield return new WaitForSeconds(Random.Range(MinLightTime, MaxLightTime));

        }
    }

}
