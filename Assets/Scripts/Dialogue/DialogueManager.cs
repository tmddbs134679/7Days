using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public string[] lines;

   [SerializeField] private UI_Dialogue dialogue;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            dialogue.SetLineAndStartDialogue(lines);
        }
    }
}
