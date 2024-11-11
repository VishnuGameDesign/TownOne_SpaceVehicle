using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    private Vector3 target_transform = new Vector3(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, -10f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target_transform) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target_transform, Vector3.Distance(transform.position, target_transform) * Time.deltaTime / 1.3f);
        }
        
    }
}
