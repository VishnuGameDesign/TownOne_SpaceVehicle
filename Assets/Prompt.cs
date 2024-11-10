using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Prompt : MonoBehaviour
{
    public string text;
    public float text_onset_speed = 4.0f;
    public float text_rise_distance = 0.1f;

    private TMP_Text textmesh;
    private float opacity;
    private bool in_prompt;
    private float base_transform_y;

    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        textmesh = this.gameObject.GetComponent<TextMeshPro>();
        base_transform_y = textmesh.transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Prompt entered");
        if (text != "") {
            textmesh.text = text;
        }
        GetComponent<Renderer>().enabled = true;
        in_prompt = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        in_prompt = false;
    }

    void Update() {
        if (in_prompt && opacity < 1.0f) {
            opacity += text_onset_speed * 0.1f;
        }
        if (!in_prompt && opacity > 0.0f) {
            opacity -= text_onset_speed * 0.1f;
        }
        if (opacity < 0.0f) {
            opacity = 0.0f;
            GetComponent<Renderer>().enabled = false;
        }
        var col = textmesh.color;
        col.a = opacity;
        textmesh.color = col;
        textmesh.transform.position = new Vector3(
            textmesh.transform.position.x,
            base_transform_y + (opacity - 1.0f) * text_rise_distance,
            textmesh.transform.position.z);

    }
}
