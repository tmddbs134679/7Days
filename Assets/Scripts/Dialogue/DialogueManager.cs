using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueSequence lines;
    private GameObject dialogueObj;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if(dialogueObj == null)
            {
                dialogueObj = UIManager.instance.ShowPopupUI("UI_Dialogue", lines).gameObject;
            }

        }
    }
}
