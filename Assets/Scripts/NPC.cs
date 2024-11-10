using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if(dialoguePanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        
    }

    public void ZeroText()
    {
        dialogueText.text = "";
        index =0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

    }

    public void NextLine()
    {
        if(index<dialogue.Length-1)
        {
            index++;
            dialogueText.text="";
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        playerIsClose = true;
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        playerIsClose = false;
        ZeroText();
    }

}
