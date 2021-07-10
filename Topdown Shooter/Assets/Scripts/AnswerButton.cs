using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public int answerIndex;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(clicked);
    }

    public void clicked()
    {
        DialogueManager.instance.choiceMade(this);
    }
}
