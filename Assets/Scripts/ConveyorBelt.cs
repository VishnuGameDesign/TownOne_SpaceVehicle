using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float BeltTiming;
    public float BeltSpeedScale;
    public Vector2 BeltDirection;
    public int NumVehicles;

    public List<GameObject> AllVehicles;

    private float BeltSpeed;
    private int state = 0;

    void Start()
    {
        InvokeRepeating("AdvanceBelt", 0.0f, BeltTiming);
    }

    void AdvanceBelt()
    {
        Debug.Log(state);
        switch(state) {
            case 0: // not moving
                BeltSpeed = 0f;
                state = 1;
                break;
            case 1: // speeding up
                BeltSpeed = 0.5f;
                state = 2;
                break;
            case 2: // in motion
                BeltSpeed = 1f;
                state = 3;
                break;
            case 3: // slowing down
                BeltSpeed = 0.5f;
                state = 4;
                break;

            case 4: // snap back and reset
                BeltSpeed = 0f;

                if (AllVehicles.Count < NumVehicles) {
                    // add a new vehicle until we have NumVehicles of them
                    GameObject clone = Object.Instantiate(AllVehicles[0], this.transform.position, Quaternion.identity);
                    AllVehicles.Add(clone);
                } else {
                    // The last one moves back to the start
                    GameObject endVehicle = AllVehicles[0];
                    AllVehicles.RemoveAt(0);
                    endVehicle.transform.position = this.transform.position;
                    AllVehicles.Add(endVehicle);
                }

                state = 0;
                break;
        }
    }

    void FixedUpdate()
    {
        for(int i = 0; i < AllVehicles.Count; i++)
        {
            GameObject Go = AllVehicles[i];

            Go.transform.position = new Vector3(
                Go.transform.position.x + BeltDirection.x * BeltSpeed * BeltSpeedScale,
                Go.transform.position.y + BeltDirection.y * BeltSpeed * BeltSpeedScale,
                Go.transform.position.z);
        } 
    }
}
