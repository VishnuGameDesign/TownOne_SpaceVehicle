using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public string testCharacterID = "AlienGreen"; // Example character ID

    void Start()
    {
        // Load the dialogue for a specific character
        dialogueManager.LoadDialogue(testCharacterID);
        dialogueManager.DisplayNextLine();  // Display the first line
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Example interaction
        {
            dialogueManager.DisplayNextLine();
        }
    }
}