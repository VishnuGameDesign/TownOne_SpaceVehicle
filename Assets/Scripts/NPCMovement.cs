using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public NPCManager my_manager;
    public Vector2 start_position;
    public Vector2 target_position;
    public string current_state;

    public float walk_speed = 5f;
    private float idle_timer = 0;
    private Vector2 target_position_offset = new Vector2(0f, 0f);
    private float timer_before_leaving = 0;

    // Start is called before the first frame update
    void Start()
    {
        current_state = "inactive";
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NPC>())
        {
            if(GetComponent<NPC>().playerIsClose == false)
            {
                switch(current_state)
                {
                    case "entering":
                        //face direction
                        if (GetComponent<SpriteRenderer>())
                        {
                            if (transform.position.x > target_position.x) GetComponent<SpriteRenderer>().flipX = true;
                            else GetComponent<SpriteRenderer>().flipX = false;
                        }

                        // move towards the target location
                        transform.position = Vector2.MoveTowards(transform.position, target_position, walk_speed * Time.deltaTime);
                        if(Vector2.Distance(transform.position, target_position) < 1f)
                        {
                            current_state = "idle";
                            idle_timer = 5f;
                            timer_before_leaving = 10f;
                        }
                        break;

                    case "idle":
                        timer_before_leaving -= Time.deltaTime;
                        if (timer_before_leaving <= 0f)
                        {
                            current_state = "leaving";
                        }
                        else
                        {
                            if (idle_timer <= 0f)
                            {
                                if (target_position_offset == new Vector2(0f, 0f))
                                {
                                    target_position_offset = new Vector2(transform.position.x + UnityEngine.Random.Range(-3.0f, 3.0f), transform.position.y + UnityEngine.Random.Range(-3.0f, 3.0f));
                                }
                                //face direction
                                if (GetComponent<SpriteRenderer>())
                                {
                                    if (transform.position.x > target_position_offset.x) GetComponent<SpriteRenderer>().flipX = true;
                                    else GetComponent<SpriteRenderer>().flipX = false;
                                }
                                
                                // move towards the target location
                                transform.position = Vector2.MoveTowards(transform.position, target_position_offset, walk_speed * Time.deltaTime);
                                if(Vector2.Distance(transform.position, target_position_offset) < 1f)
                                {
                                idle_timer = 5f;
                                }
                            }
                            else
                            {
                                idle_timer -= Time.deltaTime;
                                if (target_position_offset != new Vector2(0f, 0f))
                                {
                                    target_position_offset = new Vector2(0f, 0f);
                                }
                            }
                        }
                        break;

                    case "talking":
                        break;

                    case "leaving":
                        //face direction
                        if (GetComponent<SpriteRenderer>())
                        {
                            if (transform.position.x > start_position.x) GetComponent<SpriteRenderer>().flipX = true;
                            else GetComponent<SpriteRenderer>().flipX = false;
                        }

                        // move towards the target location
                        transform.position = Vector2.MoveTowards(transform.position, start_position, walk_speed * Time.deltaTime);
                        if(Vector2.Distance(transform.position, start_position) < 1f)
                        {
                            current_state = "inactive";
                            gameObject.SetActive(false);
                            my_manager.number_visible--;
                        }
                        break;

                    default:
                        break;

                }
            }
        }
        

    }
}
