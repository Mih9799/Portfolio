using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBotsOnly : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("AIType", 1);
    }
}
