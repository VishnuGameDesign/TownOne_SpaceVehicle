using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceVehicle : MonoBehaviour
{
    public bool Fixed = false;
    public int ToolRequired = 1;

    void ApplyTool(int i) {
        if (!Fixed &&  i == ToolRequired) {
            Debug.log("Player fixed vehicle");
            Fixed = true;
        } else if(Fixed) {
            Debug.log("Player applied tool to fixed vehicle");
        } else {
            Debug.log("Player applied wrong tool to vehicle");
        }
    }

    void Update()
    {
        if(Fixed) {
            GetComponent<Renderer>().material.color = Color.white;
        } else {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
