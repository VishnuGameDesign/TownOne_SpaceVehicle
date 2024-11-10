using UnityEngine;

public class SimpleCollisionCheck : MonoBehaviour
{
    // This function is called when another collider enters the trigger zone
    public void OnTriggerEnter(Collider other)
    {
        // Log the name of the other object that triggered the collision
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        
    }

    // This function is called when another collider stays within the trigger zone
    public void OnTriggerStay(Collider other)
    {
        // You can add logic here if you need to check while another object is inside the trigger zone
    }

    // This function is called when another collider exits the trigger zone
    public void OnTriggerExit(Collider other)
    {
        // Log the name of the other object that exited the collision
        Debug.Log("Trigger exited by: " + other.gameObject.name);
    }
}