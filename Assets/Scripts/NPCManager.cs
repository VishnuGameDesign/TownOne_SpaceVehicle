using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject[] npcs;
    public int current_npc_index = 0;
    public int number_visible = 0;
    private int number_visible_max = 3;
    public Vector2[] npc_positions;
    public int current_npc_position_index = 0;
    public Vector2[] spawn_points;
    private float spawn_timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < npcs.Length; i++)
        {
            print("Found: " + npcs[i].name);
            if (i < number_visible_max)
            {
                npc_positions[i] = npcs[i].transform.position;
            }
            if (npcs[i].GetComponent<NPCMovement>())
            {
                npcs[i].GetComponent<NPCMovement>().my_manager = this;
            }
            npcs[i].SetActive(false);
        }
        current_npc_index = 1;
        current_npc_position_index = 0;
        number_visible = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn_timer <= 0)
        {
            GameObject current_npc = npcs[current_npc_index];
            if (current_npc.GetComponent<NPCMovement>())
            {
                if (current_npc.GetComponent<NPCMovement>().current_state == "inactive")
                {
                    current_npc.transform.position = spawn_points[UnityEngine.Random.Range(0, spawn_points.Length)];
                    print(current_npc_index);
                    print(current_npc_position_index);
                    current_npc.GetComponent<NPCMovement>().start_position = current_npc.transform.position;
                    current_npc.GetComponent<NPCMovement>().target_position = npc_positions[current_npc_position_index];
                    current_npc.GetComponent<NPCMovement>().current_state = "entering";
                    current_npc.SetActive(true);

                    current_npc_index++;
                    if (current_npc_index > npcs.Length - 1) current_npc_index = 0;
                    current_npc_position_index++;
                    if (current_npc_position_index > npc_positions.Length - 1) current_npc_position_index = 0;

                    number_visible++;
                    spawn_timer = 4f * number_visible;
                }
            }
       }
       else
       {
            if (number_visible < number_visible_max)
            {
                spawn_timer -= Time.deltaTime;
            }
       }
    }

    
}


