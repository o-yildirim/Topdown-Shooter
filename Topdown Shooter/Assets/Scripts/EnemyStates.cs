using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    public enum EnemyState{Idle,Patrolling,Agressive};
    public EnemyState state;
    void Start()
    {
        state = EnemyState.Idle;
    }

}
