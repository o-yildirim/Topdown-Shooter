using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]



public class Dialogue : MonoBehaviour
{
    public bool isMainDialogueDone = false;

    public string npcName;
    [TextArea(3,10)]
    public string[] sentences;
    public Sprite npcSprite;
    [TextArea(3, 10)]
    public string[] repeatingEndDialogue;

    public void setDialogues(string[] sentences)
    {
        this.sentences = sentences;
    }

    public void setRepeatingEndDialogue(string[] repeatingEndDialogue)
    {
        this.repeatingEndDialogue = repeatingEndDialogue;
    }

    public void setName(string npcName)
    {
        this.npcName = name;
    }

    public void setSprite(Sprite npcSprite)
    {
        this.npcSprite = npcSprite;
    }

}


