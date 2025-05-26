using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UI_Dialogue : UI_Popup
{
    [SerializeField] private GameObject root;
    [SerializeField] private float charInterval = 0.05f;
    private string[] lines;

    private bool isTyping = false;
    private int currentLineIndex = 0;
    private bool waitingForNextInput = false;

    private Coroutine typingCoroutine;
    private TextMeshProUGUI dialogueTmp;

    enum TMPs
    {
        DialogueTxt,
    }

    public override void Init()
    {
        root.SetActive(true);
        base.Init();

        Bind<TextMeshProUGUI>(typeof(TMPs));

        dialogueTmp = Get<TextMeshProUGUI>((int)TMPs.DialogueTxt);
        root.SetActive(false);
    }

    private void Update()
    {
        // 추후 키 입력은 변경해야함
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                SkipTyping();
            }
            else if (waitingForNextInput)
            {
                DisplayNextLine();
            }
        }
    }
    // 다음줄 출력
    private void DisplayNextLine()
    {
        root.SetActive(true);
        if (currentLineIndex >= lines.Length)
        {
            dialogueTmp.text = "";
            ResetDialogue();
            return; // 끝남
        }

        string line = lines[currentLineIndex];
        currentLineIndex++;
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeTextCoroutine(line));
    }

    // 한글자씩 나오게 하는 코루틴
    private IEnumerator TypeTextCoroutine(string line)
    {
        isTyping = true;
        waitingForNextInput = false;
        dialogueTmp.text = "";

        foreach (char c in line)
        {
            dialogueTmp.text += c;
            yield return new WaitForSeconds(charInterval);
        }

        isTyping = false;
        waitingForNextInput = true;
        typingCoroutine = null;
    }

    // 스킵
    private void SkipTyping()
    {
        if (!isTyping) return;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueTmp.text = lines[currentLineIndex - 1];
        isTyping = false;
        waitingForNextInput = true;
        typingCoroutine = null;
    }

    private void ResetDialogue()
    {
        currentLineIndex = 0;
        this.lines = null;
        root.SetActive(false);
    }

    public void SetLineAndStartDialogue(string[] lines)
    {
        if(this.lines == null)
        {
            this.lines = lines;
            DisplayNextLine();
        }
    }
}
