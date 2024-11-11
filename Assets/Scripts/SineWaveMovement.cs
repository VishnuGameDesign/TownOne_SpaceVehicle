using UnityEngine;

public class SineWaveMovement : MonoBehaviour
{
    // Parameters for controlling the movement
    public float amplitude = 1.0f;  // Height of the wave
    public float frequency = 1.0f;  // Speed of the wave
    public Vector2 direction = new Vector2(0f, 1.0f);

    // Initial position of the object
    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position using a sine wave
        float newY = startPosition.y + (Mathf.Sin(Time.time * frequency) * amplitude * direction.y);
        float newX = startPosition.x + (Mathf.Cos(Time.time * frequency) * amplitude * direction.x);
        
        // Update the object's position
        transform.position = new Vector3(newX, newY, startPosition.z);
    }
}
