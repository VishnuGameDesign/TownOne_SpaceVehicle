using UnityEngine;
using System.Collections.Generic;

public class DialogueData
{
    public string UID { get; set; }
    public string CharacterID { get; set; }
    public string Line { get; set; }
    
}

public class DialogueManager : MonoBehaviour
{
    public List<DialogueData> dialogues = new List<DialogueData>();
    private int currentLineIndex = 0;

    public void LoadDialogue(string characterID)
    {
        dialogues.Clear();
        for (int i = 0; i < CSVReader.Instance.convertedData.Count; i++)
        {
            if (CSVReader.Instance.convertedData[i]["CharacterID"] == characterID)
            {
                DialogueData data = new DialogueData
                {
                    UID = CSVReader.Instance.convertedData[i]["UID"],
                    CharacterID = CSVReader.Instance.convertedData[i]["CharacterID"],
                    Line = CSVReader.Instance.convertedData[i]["Line"]
                    // Add other properties as needed
                };
                dialogues.Add(data);
            }
        }
    }

    public void DisplayNextLine()
    {
        if (currentLineIndex < dialogues.Count)
        {
            DialogueData data = dialogues[currentLineIndex];
            Debug.Log(data.Line);
            currentLineIndex++;
        }
        else
        {
            Debug.Log("End of dialogue.");
        }
    }

    public void ResetDialogue()
    {
        currentLineIndex = 0;
    }
}   