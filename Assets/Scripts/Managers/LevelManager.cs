using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int[] moveCount;

    private void Awake()
    {
        instance = this;
    }
    public int HighestMoveCount()
    {
        return moveCount[2];
    }
    public int ReturnNextMoveCount(int num)
    {
        if (num < moveCount[0]) return moveCount[0];
        else if (num >= moveCount[0] && num < moveCount[1]) return moveCount[1];
        else return moveCount[2];
    }
    public int ReturnClearStar(int num)
    {
        if(num <= moveCount[0])
        {
            return 3;
        }else if (num > moveCount[0] && num <= moveCount[1])
        {
            return 2;
        }
        else if (num > moveCount[1] && num <= moveCount[2])
        {
            return 1;
        }else return 0;
    }
}
