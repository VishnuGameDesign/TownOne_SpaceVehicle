using System.Collections;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;

    private Coroutine typingCoroutine;

    public GameObject eHoverIcon;

    void Update()
    {
        AnimaleseSpeaker speaker = GetComponent<AnimaleseSpeaker>();
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                    typingCoroutine = null;
                    dialogueText.text = dialogue[index];
                }
                else
                {
                    NextLine();
                }
            }
            else
            {
                // Start the dialogue
                speaker.SetTextToSpeak(dialogue[index]);
                speaker.SetPlaybackTimeBetweenLetters(wordSpeed);
                speaker.StartPlayback();
                dialoguePanel.SetActive(true);

                // Hide the E hover icon
                if (eHoverIcon != null)
                {
                    eHoverIcon.SetActive(false);
                }

                typingCoroutine = StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            speaker.StopPlayback();
        }
    }

    public void ZeroText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

        if (playerIsClose && eHoverIcon != null)
        {
            eHoverIcon.SetActive(true);
        }
    }

    IEnumerator Typing()
    {

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        typingCoroutine = null; // Typing is finished
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            typingCoroutine = StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;

            // Show the E hover icon if dialogue is not active
            if (!dialoguePanel.activeInHierarchy && eHoverIcon != null)
            {
                eHoverIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            ZeroText();

            if (eHoverIcon != null)
            {
                eHoverIcon.SetActive(false);
            }
        }
        AnimaleseSpeaker speaker = GetComponent<AnimaleseSpeaker>();
        speaker.StopPlayback();
    }
}