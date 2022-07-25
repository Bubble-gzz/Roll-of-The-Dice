using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;
    float size = 1.0f,leftBorder,upBorder;
    public int Row = 5, Col = 5;
    //Color colorA, colorB;
    public GameObject[,] tileObjects;
    public int [,] tileType;
    [SerializeField]
    Material [] materials;
    [SerializeField]
    public int currentPuzzle;
    List<Puzzle> puzzles = new List<Puzzle>();
    [SerializeField]
    public Vector2 startPos;
    [SerializeField]
    public Vector3 startPosition;
    public Vector3 mapPivot;
    [SerializeField]
    public int restCheckCount;
    public bool selectPuzzle;

    [SerializeField]
    Text levelText;
    [SerializeField]
    Image switchingImage;

    void Awake()
    {
        InitializeMaps();
        if (!selectPuzzle) currentPuzzle = PuzzleManager.currentPuzzle;
        //currentPuzzle = 1;

        Row = puzzles[currentPuzzle].row;
        Col = puzzles[currentPuzzle].col;
        startPos = puzzles[currentPuzzle].startPos;
        
        if (currentPuzzle == 0)
            levelText.text = "";
        else
        {
            StartCoroutine(SwitchingEffect());
            levelText.text = puzzles[currentPuzzle].title;
        }
        //Debug.Log(currentPuzzle);
        //Debug.Log(puzzles[currentPuzzle].map[0,0]);


        tileObjects = new GameObject[Row, Col];
        tileType = new int[Row, Col];
        for (int i=0; i<Row; i++)
            for (int j=0; j<Col; j++)
                tileType[i,j] = puzzles[currentPuzzle].map[i,j];

        leftBorder = 0; upBorder = 0; restCheckCount = 0;
        startPosition = new Vector3(upBorder + startPos.y * size, 1.0f, leftBorder + startPos.x * size);
        mapPivot = new Vector3((upBorder + Col * size) / 2.0f, 0, (leftBorder + Row * size) / 2.0f);
        for (int i=0; i<Row; i++)
            for (int j=0; j<Col; j++)
            if (tileType[i,j] != 0)
            {
                if (tileType[i,j] > 1 && tileType[i,j] < 13) restCheckCount++;
                tileObjects[i,j] = Instantiate(tilePrefab, new Vector3(upBorder + j*size, 0, leftBorder + i * size), Quaternion.identity);
                tileObjects[i,j].GetComponent<Renderer>().material = materials[tileType[i,j]];
                //tileObjects[i,j].GetComponent<Renderer>().material.color = GetColor(i,j);
            }
    }

/*    Color GetColor(int i,int j)
    {
        if (i % 2 + j % 2 == 1) return colorA;
        else return colorB;
    }
*/
    public void FinishTile(int x,int y,int newType)
    {
        GameObject tile = tileObjects[x,y];
        tileType[x,y] = newType;
        tile.GetComponent<Renderer>().material = materials[newType];
        restCheckCount--;
    }
    public void nextPuzzle()
    {
        PuzzleManager.FinishPuzzle(currentPuzzle);
        if (currentPuzzle < puzzles.Count - 1) {
            PuzzleManager.currentPuzzle++;
            selectPuzzle = false;
            SceneManager.LoadScene(PuzzleManager.nameOfMainScene);
        }
    }
    void InitializeMaps()
    {
        puzzles.Add(new Puzzle("",10, 10, new int[10, 10] //puzzle-0
        {
            {1,1,1,1,1,1,6,0,0,0},
            {1,0,0,0,0,0,1,0,0,0},
            {1,0,0,0,0,0,1,0,0,0},
            {1,0,0,0,0,0,1,0,0,0},
            {1,0,0,0,0,0,1,0,0,0},
            {1,0,0,0,0,0,1,0,1,1},
            {6,1,1,1,1,1,21,0,0,0},
            {0,0,0,0,0,1,0,0,0,0},
            {0,0,0,0,0,1,0,0,0,0},
            {0,0,0,0,0,1,0,0,0,0},
        }));
        puzzles.Add(new Puzzle("Light Them Up",5,5,new int[5,5]{ //puzzle-1
            {1,1,1,1,12},
            {1,1,1,1,1},
            {1,1,21,1,1},
            {1,1,1,1,1},
            {4,1,1,1,6}    
        }));
        puzzles.Add(new Puzzle("22223333",2,8,new int[2,8]{ //puzzle-2
            {1,6,1,6,1,6,1,6},
            {4,1,4,1,4,1,4,21}
        }));
        puzzles.Add(new Puzzle("This is Roll 2",7,5,new Vector2(1,1),new int[7,5]{ // puzzle-3
            {0,21,0,0,0},
            {0,1,0,0,0},
            {0,1,0,0,0},
            {4,14,1,1,4},
            {0,1,0,0,0},
            {0,1,0,0,0},
            {0,4,0,0,0}
        }));
        puzzles.Add(new Puzzle("pracDice Makes Perfect",5,5,new int[5,5]{ //puzzle-4
            {1,1,1,8,0},
            {1,1,0,1,0},
            {6,1,1,14,1},
            {1,0,0,1,1},
            {1,2,1,4,21}
        }));
        puzzles.Add(new Puzzle("Quite Straightforward",1,11,new int[1,11]{ //puzzle-5
            {1,8,4,14,14,14,14,14,4,10,21}
        }));
        puzzles.Add(new Puzzle("Death Desu",4,9,new int[4,9]{ //puzzle-6
            {1,17,1,1,1,1,1,17,10},
            {1,0,20,4,0,15,4,0,1},
            {1,0,4,16,0,4,15,0,1},
            {10,16,1,1,1,1,1,17,21}
        }));

        puzzles.Add(new Puzzle("Nice Size Dice",4,4,new int[4,4]{ //puzzle-7
            {1,12,12,12},
            {12,12,12,12},
            {12,12,12,12},
            {12,12,12,21}
        }));
        puzzles.Add(new Puzzle("Happy Road",6,11,new int[6,11]{ //puzzle-8
            {1,1,17,1,16,19,1,17,17,17,1},
            {0,14,0,14,0,0,14,0,0,0,1},
            {0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0},
            {14,17,17,17,17,14,17,17,17,17,21}
        }));
        
        puzzles.Add(new Puzzle("Take Turns Alone",5,5,new int[5,5]{ //puzzle-9
            {1,2,14,4,14},
            {2,14,4,14,6},
            {14,4,14,6,14},
            {4,14,6,14,8},
            {14,6,14,8,21}
        }));
        /*puzzles.Add(new Puzzle("",5,5,new int[5,5]{ //puzzle-3
            {1,1,1,1,1},
            {0,0,0,1,1},
            {0,0,0,1,2},
            {0,0,0,0,1},
            {0,0,0,0,21}
        }));
        */
        puzzles.Add(new Puzzle("Mine Area",5,5,new int[5,5]{ // puzzle-10
            {1,17,0,17,14},
            {17,1,17,1,17},
            {0,0,8,0,1},
            {17,1,18,1,17},
            {21,17,0,17,6}    
        }));
        puzzles.Add(new Puzzle("Thanks for playing our demo :)",5,5,new int[5,5]{ // puzzle-11
            {1,1,1,1,1},
            {1,1,1,1,1},
            {1,1,1,1,1},
            {1,1,1,0,0},
            {1,1,1,0,0}    
        }));

        if (JsonTest.Read().clearNum >= puzzles.Count - 1)
        {
            puzzles[0] = new Puzzle("", 10, 10, new int[10, 10] //puzzle-0
            {
                {1,1,1,1,1,1,6,0,0,0},
                {1,0,0,0,0,0,1,0,0,0},
                {1,0,0,0,0,0,1,0,0,0},
                {1,0,0,0,0,0,1,0,0,0},
                {1,0,0,0,0,0,1,0,0,0},
                {1,0,0,0,0,0,1,1,1,1},
                {6,1,1,1,1,1,21,0,0,0},
                {0,0,0,0,0,1,0,0,0,0},
                {0,0,0,0,0,1,0,0,0,0},
                {0,0,0,0,0,1,0,0,0,0},
            });
        }

        Debug.Log(puzzles.Count);
    }
    
    IEnumerator SwitchingEffect()
    {
        switchingImage.enabled = true;
        float imageA = 0.8f;
        while(switchingImage.color.a >= 0)
        {
            switchingImage.color = new Color(0, 0, 0, imageA);
            imageA -= Time.deltaTime;
            yield return null;
        }
        switchingImage.color = new Color(0, 0, 0, 0);
        switchingImage.enabled = false;
    }
}
