using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BASE CLASS: will derive from this class (abstract)
public abstract class AI_Perception : MonoBehaviour
{   // makes public 
    [SerializeField] string tagName = "";
    [SerializeField] float distance = 1;
    [SerializeField] float maxAngle = 45; // degrees

    public string TagName { get { return tagName; } }
    public float Distance { get { return distance; } }
    public float MaxAngle { get { return maxAngle; } }

    // derived classes will implement this method 
    public abstract GameObject[] GetGameObjects();
}
