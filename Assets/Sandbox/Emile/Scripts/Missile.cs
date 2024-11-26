using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    
    [SerializeField] public Transform target;
    void Update()
    {
        transform.LookAt(target);
    }
}
