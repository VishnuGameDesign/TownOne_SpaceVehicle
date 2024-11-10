using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Prompt : MonoBehaviour
{
    public string text;
    private TMP_Text textmesh;

    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        textmesh = this.gameObject.GetComponent<TextMeshPro>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Prompt entered");
        if (text != "") {
            textmesh.text = text;
        }
        GetComponent<Renderer>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<Renderer>().enabled = false;
    }
}
