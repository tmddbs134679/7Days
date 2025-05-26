using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UI_Dialogue : UI_Popup
{
    [SerializeField] private float charInterval = 0.05f;
    private string[] lines;

    [SerializeField] private bool isTyping = false;
    [SerializeField] private int currentLineIndex = 0;
    private bool waitingForNextInput = false;

    private Coroutine typingCoroutine;
    private TextMeshProUGUI dialogueTmp;

    enum TMPs
    {
        DialogueTxt,
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
        if (currentLineIndex >= lines.Length)
        {
            dialogueTmp.text = "";
            UIManager.instance.ClosePopupUI(this);
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
            yield return new WaitForSecondsRealtime(charInterval);
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

    public void SetLineAndStartDialogue(string[] lines)
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(TMPs));
        dialogueTmp = Get<TextMeshProUGUI>((int)TMPs.DialogueTxt);

        this.lines = lines;
        currentLineIndex = 0;
        DisplayNextLine();
    }
}
