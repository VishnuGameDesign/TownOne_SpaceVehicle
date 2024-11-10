using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float BeltTiming;
    public float BeltRampUpSpeed;
    public float BeltSpeed;
    public Vector2 BeltDirection;

    void Start()
    {
        InvokeRepeating("StartToggleBelt", 0.0f, BeltTiming);
    }

    void ToggleBelt()
    {

    }

    void FixedUpdate()
    {
        for(int i = 0; i < this.gameobject.transform.GetChildCount(); i++)
        {
            GameObject Go = this.gameobject.transform.GetChild(i);

            Go.transform = new Vector3(
                Go.transform.x + BeltDirection.x * BeltSpeed,
                Go.transform.y + BeltDirection.y * BeltSpeed,
                Go.transform.z);
        } 
    }
}
