using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public AbstractDungeonGenerator dungeonGenerator;

    private void Awake()
    {
        if(dungeonGenerator != null) dungeonGenerator.GenerateDungeon();
    }
}
