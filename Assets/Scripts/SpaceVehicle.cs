using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceVehicle : MonoBehaviour
{
    public bool Fixed = false;
    public float ZotFlashTime = 0.5f;
    public int ToolRequired = 2;
    public Color FixedColor;

    public GameObject zot;

    public void ApplyTool(int i) {
        if (!Fixed &&  i == ToolRequired) {
            Debug.Log("Player fixed vehicle");
            Fixed = true;
        } else if(Fixed) {
            Debug.Log("Player applied tool to fixed vehicle");
        } else {
            Debug.Log("Player applied wrong tool to vehicle");
            zot.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(HideZot());
        }
    }

    IEnumerator HideZot() {
        yield return new WaitForSeconds(ZotFlashTime);
        zot.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if(Fixed) {
            GetComponent<Renderer>().material.color = FixedColor;
        } else {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
