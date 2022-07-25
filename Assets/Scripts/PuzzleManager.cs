using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager
{
    // Start is called before the first frame update
    public static int currentPuzzle = 0;
    public static string nameOfMainScene = "SampleScene";
    public static void FinishPuzzle(int currentPuzzle)
    {
        if (JsonTest.Read().clearNum < currentPuzzle)
            JsonTest.Save(new LevelInfo(currentPuzzle));
    }
}
