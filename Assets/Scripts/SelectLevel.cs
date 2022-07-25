using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private MapManager mapManager;
    private Button[] buttons;

    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i <= JsonTest.Read().clearNum + 1; i++)
        {
            buttons[i].interactable = true;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int j = i;
            buttons[i].onClick.AddListener(() => JumpLevel(j));
        }
    }

    private void JumpLevel(int level)
    {
        PuzzleManager.currentPuzzle = level;
        mapManager.selectPuzzle = false;
        SceneManager.LoadScene(PuzzleManager.nameOfMainScene);
        Debug.Log(level);
    }
}
