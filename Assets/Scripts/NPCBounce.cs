using UnityEngine;

public class NPCBounce : MonoBehaviour
{
    public float bounceAmplitude = 0.1f; // Height of the bounce
    public float bounceFrequency = 2f;   // Speed of the bounce

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the NPC
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;

        // Update the NPC's position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}