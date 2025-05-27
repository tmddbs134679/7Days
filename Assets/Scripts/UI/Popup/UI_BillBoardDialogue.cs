using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_BillBoardDialogue : UI_Popup
{
    [SerializeField] private float charInterval = 0.05f;
    private DialogueLine[] lines;

    [SerializeField] private bool isTyping = false;
    [SerializeField] private int currentLineIndex = 0;
    private bool waitingForNextInput = false;

    private Coroutine typingCoroutine;
    private TextMeshProUGUI dialogueTmp;


    public RectTransform rect;
    private CanvasGroup canvasGroup;

    enum TMP
    {
        DialogueTxt,
        NameTxt
    }
    enum Images
    {
        PlayerImage,
        NpcImage
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
            AnimateOut();
            return; // 끝남
        }
        ResetDetails();

        string line = lines[currentLineIndex].dialogueText;
        //이미지와 name박스 설정
        SetDetails();

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

        dialogueTmp.text = lines[currentLineIndex - 1].dialogueText;
        isTyping = false;
        waitingForNextInput = true;
        typingCoroutine = null;
    }

    public void SetLineAndStartDialogue(DialogueSequence lines)
    {
        base.Init();
        rect = transform.GetChild(0).GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        Bind<TextMeshProUGUI>(typeof(TMP));
        Bind<Image>(typeof(Images));
        dialogueTmp = Get<TextMeshProUGUI>((int)TMP.DialogueTxt);
        nameTmp = Get<TextMeshProUGUI>((int)TMP.NameTxt);

        playerImage = Get<Image>((int)Images.PlayerImage);
        npcImage = Get<Image>((int)Images.NpcImage);
        ResetDetails();

        this.lines = lines.dialogueLines;
        currentLineIndex = 0;

        AnimateIn();

    }

    // 이미지와, 이름 초기화(setFalse)
    void ResetDetails()
    {
        nameTmp.transform.parent.gameObject.SetActive(false);
        playerImage.gameObject.SetActive(false);
        npcImage.gameObject.SetActive(false);
    }



}
