using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialogueCanvas;
    public Text dialogueDisplayText;
    public Image npcSpriteImage;
    public Text npcNameText;
    public Animator dialogueCanvasAnimator;
    public Button[] choiceButtons;

    private Dictionary<string, string> dialogues;
    private int dialogueIndex;
    private int selectedButtonIndex;
    private Dialogue currentDialogue;

    private bool isInDialogue = false;
    private bool answerGiven = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        dialogues = new Dictionary<string, string>();
    }

    public void startDialogue(Dialogue dialogue)
    {
        if (isInDialogue) return;

        isInDialogue = true;
        GameController.instance.isGamePaused = true;


        npcNameText.text = dialogue.npcName;
        npcSpriteImage.sprite = dialogue.npcSprite;
        dialogueCanvas.SetActive(true);
        dialogueCanvasAnimator.SetBool("isOpen", true);

        dialogues.Clear();

        currentDialogue = dialogue;
        string[] sentences;
        if (currentDialogue.isMainDialogueDone)
        {
            sentences = currentDialogue.repeatingEndDialogue;
        }
        else
        {
            sentences = currentDialogue.sentences;         
        }


        foreach (string sentence in sentences)
        {
            string keyOfTheSentence = "";

            int spaceCharIndex = sentence.IndexOf(' ');
            keyOfTheSentence = sentence.Substring(0, spaceCharIndex);
            string sentenceIntoDictionary = sentence.Substring(spaceCharIndex + 1, sentence.Length - spaceCharIndex - 1);
            dialogues.Add(keyOfTheSentence, sentenceIntoDictionary);
        }

        StopAllCoroutines();
        StartCoroutine(displaySentences());

    }



    public IEnumerator displaySentences()
    {
        dialogueDisplayText.text = "";
        while (dialogueCanvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueCanvasClosed"))
        {
            yield return null;
        }

        dialogueIndex = 0;

        string sentence;
        while (dialogues.TryGetValue("/D" + dialogueIndex, out sentence))
        {
            dialogueDisplayText.text = "";

            foreach (char letter in sentence.ToCharArray())
            {
                if (Input.GetMouseButtonDown(0) && !answerGiven)
                {
                    dialogueDisplayText.text = sentence;
                    yield return null;
                    break;
                }
                dialogueDisplayText.text += letter;
                yield return null;
            }
            string[] possibleSentences = new string[choiceButtons.Length];
            if (!answerableDialogue(possibleSentences))
            {
                while (!Input.GetMouseButtonDown(0))
                {
                    yield return null;
                }
            }
            else
            {
                answerGiven = false;
                //Buton basılana kadar bekleyecek;
            
                for (int i = 0; i < possibleSentences.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(possibleSentences[i]))
                    {
                        choiceButtons[i].transform.GetComponentInChildren<Text>().text = possibleSentences[i];
                        choiceButtons[i].gameObject.SetActive(true);
                    }
                }
                while (!answerGiven)
                {
                    yield return null;
                }

                disableAnswerButtons();

                string counterAnswer = "";
                if (counterAnswerAvailable(ref counterAnswer))
                {
                    dialogueDisplayText.text = "";
                    foreach (char letter in counterAnswer.ToCharArray())
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            dialogueDisplayText.text = counterAnswer;
                            yield return null;
                            break;
                        }
                        dialogueDisplayText.text += letter;
                        yield return null;
                    }

                    while (!Input.GetMouseButtonDown(0))
                    {
                        yield return null;
                    }
                }

            }

            yield return null;
            dialogueIndex++;
        }
        StartCoroutine(endDialogue());
    }


    public IEnumerator endDialogue()
    {
        currentDialogue.isMainDialogueDone = true;
        dialogueCanvasAnimator.SetBool("isOpen", false);
        while (dialogueCanvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueCanvasOpening"))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        dialogueCanvas.SetActive(false);
        isInDialogue = false;
        GameController.instance.isGamePaused = false;
    }

    public bool answerableDialogue(string[] possibleSentences)
    {
        bool flag = false;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            string keyToSearch = "/A" + dialogueIndex.ToString() + i.ToString();
            if (dialogues.TryGetValue(keyToSearch, out possibleSentences[i]))
            {
                flag = true;
            }
        }
        return flag;
    }

    public void choiceMade(AnswerButton buttonSelected)
    {
        selectedButtonIndex = buttonSelected.answerIndex;
        answerGiven = true;
    }

    public bool counterAnswerAvailable(ref string counterAnswer)
    {
        bool flag = false;

        string counterAnswerKey = "/C" + dialogueIndex.ToString() + selectedButtonIndex;

        if (dialogues.TryGetValue(counterAnswerKey, out counterAnswer))
        {
            flag = true;
        }
        return flag;
    }

    public void disableAnswerButtons()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
    }

    public bool isAnyDialogueActive()
    {
        return isInDialogue;
    }

}