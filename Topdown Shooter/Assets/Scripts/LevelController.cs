using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LevelController
{
    void onLevelLoad();
    IEnumerator startLevel();
    void endLevel();
}
