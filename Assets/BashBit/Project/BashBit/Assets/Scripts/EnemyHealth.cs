using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject AISource;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TakeDamage(float amount)
    {
        GreyboxSentryAI HealthScript1 = AISource.GetComponent<GreyboxSentryAI>();
        if (HealthScript1)
        {
            HealthScript1.RegisterDamage(amount);
        }
        HitMeDoor HealthScript2 = AISource.GetComponent<HitMeDoor>();
        if (HealthScript2)
        {
            HealthScript2.RegisterDamage(amount);
        }
        GreyboxHovererAI HealthScript3 = AISource.GetComponent<GreyboxHovererAI>();
        if (HealthScript3)
        {
            HealthScript3.RegisterDamage(amount);
        }
        MinibossSentry HealthScript4 = AISource.GetComponent<MinibossSentry>();
        if (HealthScript4)
        {
            HealthScript4.RegisterDamage(amount);
        }
        HitMeSecret HealthScript5 = AISource.GetComponent<HitMeSecret>();
        if (HealthScript5)
        {
            HealthScript5.RegisterDamage(amount);
        }
        TrainerBotAI HealthScript6 = AISource.GetComponent<TrainerBotAI>();
        if (HealthScript6)
        {
            HealthScript6.RegisterDamage(amount);
        }
        TurretAI HealthScript7 = AISource.GetComponent<TurretAI>();
        if (HealthScript7)
        {
            HealthScript7.RegisterDamage(amount);
        }
        DoorGuardAI HealthScript8 = AISource.GetComponent<DoorGuardAI>();
        if (HealthScript8)
        {
            HealthScript8.RegisterDamage(amount);
        }
        DoorMinibossAI HealthScript9 = AISource.GetComponent<DoorMinibossAI>();
        if (HealthScript9)
        {
            HealthScript9.RegisterDamage(amount);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
