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

    private Queue<string> sentences;
    private bool isInDialogue = false;
    
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
        sentences = new Queue<string>();
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


        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
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

        while (sentences.Count != 0)
        {
            dialogueDisplayText.text = "";

            string sentence = sentences.Dequeue();
            foreach (char letter in sentence.ToCharArray())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    dialogueDisplayText.text = sentence;
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
            yield return null;
        }
   
        dialogueCanvasAnimator.SetBool("isOpen", false);
        while (dialogueCanvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueCanvasOpening"))
        {
            yield return null;
        }
        endDialogue();
     

    }

    public void endDialogue()
    {
        dialogueCanvas.SetActive(false);
        isInDialogue = false;
        GameController.instance.isGamePaused = false;
    }

    /*

    public void startDialogue(Dialogue dialogue)
    {
        if (isInDialogue) return;

        isInDialogue = true;

        GameController.instance.isGamePaused = true;

        dialogueCanvas.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        displayNextSentence(); //BUNU KORUTINE DONUSTURUP HEPSINI GOSTER O ZAMAN OLUR GIBI
    }

    public void displayNextSentence()
    {
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }

        string sentenceToDisplay = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(displaySentence(sentenceToDisplay));

    }

    public IEnumerator displaySentence(string sentence)
    {
        dialogueDisplayText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogueDisplayText.text = sentence;
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

        displayNextSentence();

    }

    public void endDialogue()
    {
        dialogueCanvas.SetActive(false);
        isInDialogue = false;
        GameController.instance.isGamePaused = false;
    }


    */










    /*



    public void initDialogue(Queue<string> sentences, float letterDelay, float delayBetweenSentences)
    {
        if (isInDialogue) return;
       // dialogueCoroutine = StartCoroutine(displayAllDialogue(sentences, letterDelay, delayBetweenSentences));
    }

    private void Update()
    {
        if (isInDialogue)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                disableSpeechCanvas();
                StopCoroutine(dialogueCoroutine);
                GameController.instance.isGamePaused = false;
                isInDialogue = false;

            }
        }
    }
    /*
      public IEnumerator displayAllDialogue(Queue<string> sentences, float letterDelay, float delayBetweenSentences)
      {
          GameController.instance.isGamePaused = true;
          isInDialogue = true;
          enableSpeechCanvas();
          for (int i = 0; i < sentences.Count; i++)
          {
              dialogueDisplayText.text = "";
              for (int j = 0; j < sentences[i].Length; j++)
              {
                  if (Input.GetMouseButtonDown(0))
                  {
                      dialogueDisplayText.text = sentences[i];
                      continue;
                  }
                  else
                  {
                      dialogueDisplayText.text += sentences[i][j];
                      if (sentences[i][j] == '.')
                      {
                          yield return new WaitForSeconds(delayBetweenSentences);
                      }
                      else
                      {
                          yield return new WaitForSeconds(letterDelay);
                      }
                  }
                  yield return null;
              }
              while (!Input.GetMouseButtonDown(0))
              {
                  yield return null;
              }

              yield return null;
          }
          dialogueDisplayText.text = "";
          disableSpeechCanvas();
          isInDialogue = false;
          GameController.instance.isGamePaused = false; ;

      }
      
      
    public IEnumerator displayAllDialogue(List<string> sentences, float letterDelay, float delayBetweenSentences)
    {
        GameController.instance.isGamePaused = true;
        isInDialogue = true;
        enableSpeechCanvas();

        while (sentences.Count > 0)
        {
            dialogueDisplayText.text = "";
            int j = 0;
            while (j < sentences[0].Length)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    dialogueDisplayText.text = sentences[0];
                    sentences.RemoveAt(0);
                    continue;
                }
                else
                {
                    dialogueDisplayText.text += sentences[0][j];
                    if (sentences[0][j] == '.')
                    {
                        yield return new WaitForSeconds(delayBetweenSentences);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);
                    }
                    j++;
                }

      
                yield return null;
            }
            sentences.RemoveAt(0);
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            yield return null;
        }
        dialogueDisplayText.text = "";
        disableSpeechCanvas();
        isInDialogue = false;
        GameController.instance.isGamePaused = false; 

    }

    
    public void enableSpeechCanvas()
    {
        dialogueCanvas.SetActive(true);

    }

    public void disableSpeechCanvas()
    {
        dialogueCanvas.SetActive(false);
    }
    */
}

