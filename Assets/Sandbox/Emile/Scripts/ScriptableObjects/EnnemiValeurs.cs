using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Valeurs/Vie", order = 1)]
public class EnnemiValeurs : ScriptableObject
{
    public float VieDepart;
    public float SpeedDepart;
}