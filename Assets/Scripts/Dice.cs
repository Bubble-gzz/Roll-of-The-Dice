using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dice : MonoBehaviour
{
    // Start is called before the first frame update
    const int Top = 4;
    const int Bottom = 5;
    const int FrontLeft = 0;
    const int FrontRight = 1;
    const int BackLeft = 2;
    const int BackRight = 3;
    Vector2 FrontLeftVec = new Vector2(0,-1);
    
    Vector2 FrontRightVec = new Vector2(-1,0);
    
    Vector2 BackLeftVec = new Vector2(1,0);
    
    Vector2 BackRightVec = new Vector2(0,1);
    int[] point = new int[6];
    bool rotateFreeze;
    bool endFreeze;
    float rotateFreezeTime;
    [SerializeField]
    GameObject topPoint;
    const float size = 1.0f;
    MapManager map;
    SoundManager soundManager;
    Vector2 pos;

    Vector3[] rotates = new Vector3[4]{
        new Vector3(0,0,90), //FrontLeft
        new Vector3(-90,0,0), //FrontRight
        new Vector3(90,0,0), //BackLeft
        new Vector3(0,0,-90) //BackRight
    };
    Vector3[] transfers = new Vector3[4]{
        new Vector3(-size,0,0), //FrontLeft
        new Vector3(0,0,-size), //FrontRight
        new Vector3(0,0,size), //BackLeft
        new Vector3(size,0,0) //BackRight        
    };
    Vector2[] deltaPos = new Vector2[4]{
        new Vector2(0,-1), //FrontLeft
        new Vector2(-1,0), //FrontRight
        new Vector2(1,0), //BackLeft
        new Vector2(0,1) //BackRight   
    };

    [SerializeField]
    GameObject rotatePivot;

    [SerializeField]
    Title title;

    void Start()
    {
    /*
        Color color = GetComponent<Renderer>().material.color;
        color.a = 0.1f;
        GetComponent<Renderer>().material.color = color;
    */
        map = GameObject.Find("MapManager").GetComponent<MapManager>();
        soundManager = GetComponent<SoundManager>();
        rotatePivot = GameObject.Find("RotatePivot");
        pos = map.startPos;//new Vector2(0,0);
        transform.position = map.startPosition;//new Vector3(0,1f,0);
    
        point[Top] = 1;
        point[Bottom] = 3;
        point[FrontLeft] = 2;
        point[FrontRight] = 6;
        point[BackLeft] = 4;
        point[BackRight] = 5;
        rotateFreeze = false;
        endFreeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotateFreeze && !endFreeze)
        {
            int dir = -1;

            if (Input.GetKeyDown(KeyCode.Q)) dir = BackLeft;
            else if (Input.GetKeyDown(KeyCode.Z)) dir = FrontLeft;
            else if (Input.GetKeyDown(KeyCode.M)) dir = FrontRight;
            else if (Input.GetKeyDown(KeyCode.P)) dir = BackRight;
            if (dir != -1) StartCoroutine(Rotate(dir));
        }
    }
    IEnumerator Rotate(int dir)
    {
        Vector2 _pos = pos + deltaPos[dir];
        if (_pos.x < 0 || _pos.x >= map.Row) yield break; 
        if (_pos.y < 0 || _pos.y >= map.Col) yield break;
        if (map.tileType[(int)_pos.x, (int)_pos.y] == 0) yield break;
        switch (dir) {
            case FrontLeft:
                Revolve(ref point[FrontLeft],ref point[Top],ref point[BackRight],ref point[Bottom]);
                break;
            case FrontRight:
                Revolve(ref point[FrontRight],ref point[Top],ref point[BackLeft],ref point[Bottom]);
                break;
            case BackLeft:
                Revolve(ref point[BackLeft],ref point[Top],ref point[FrontRight],ref point[Bottom]);
                break;
            case BackRight:
                Revolve(ref point[BackRight],ref point[Top],ref point[FrontLeft],ref point[Bottom]);
                break;
        }
        yield return RotationAnimation(dir, 50);
        pos = _pos;
        yield return Interact(50);
        rotateFreeze = false;
    }
    IEnumerator RotationAnimation(int dir, int iteration)
    {
        rotateFreeze = true;
        Vector3 _position = transform.position + transfers[dir];
        transform.Rotate(rotates[dir].x, rotates[dir].y, rotates[dir].z, Space.World);
        Quaternion _rotation = transform.rotation;
        transform.Rotate(-rotates[dir].x, -rotates[dir].y, -rotates[dir].z, Space.World);
        Transform _parent = transform.parent;

        rotatePivot.transform.position = (_position + transform.position) / 2 - new Vector3(0,0.5f,0);
        transform.parent = rotatePivot.transform;
        float _rx = rotates[dir].x / iteration;
        float _ry = rotates[dir].y / iteration;
        float _rz = rotates[dir].z / iteration;
        for (int i = 0; i < iteration; i++)
        {
            rotatePivot.transform.Rotate(_rx, _ry, _rz, Space.World);
            yield return null;
        }
        //Debug.Log(_parent);
        transform.parent = _parent;
    
        //transform.position = transform.position + transfers[dir];
        transform.position = _position;
        transform.rotation = _rotation;
//        transform.Rotate(rotates[dir].x, rotates[dir].y, rotates[dir].z, Space.World);
        

        yield return new WaitForSeconds(rotateFreezeTime);
        if (topPoint != null) topPoint.GetComponent<Text>().text = "Point = " + point[Top].ToString();
    }
    void Revolve(ref int a, ref int b, ref int c, ref int d)
    {
        int tmp = a;
        a = b;
        b = c;
        c = d;
        d = tmp;
    }
    IEnumerator Interact(int iteration)
    {
        int tileType = map.tileType[(int)pos.x, (int)pos.y];
        if (15 <= tileType && tileType <= 20)
        {
            int dots = tileType - 14;
            if (point[Top] == dots) {
                StartCoroutine(OnGameOver());
                map.tileObjects[(int)pos.x, (int)pos.y].GetComponent<Cube>().Vanish();
                yield break;
            }
        }
        soundManager.PlaySound("roll");
        if (tileType > 1 && tileType < 13)
        {
            if (tileType % 2 == 0)
            {
                int dots = tileType/2;
                if (point[Top] == dots)
                {
                    soundManager.PlaySound("bingo");
                    map.FinishTile((int)pos.x, (int)pos.y, tileType + 1);
                }
            }
        }
        if (tileType == 14)
        {
            yield return RotateClockWise(iteration);
        }
        if (tileType == 21) //destination
        {
            if (map.restCheckCount < 1)
            {
                Debug.Log("Puzzle" + map.currentPuzzle.ToString() +" solved.");
                StartCoroutine(PuzzleSolved());
            }
        }
    }
    IEnumerator RotateClockWise(int iteration)
    {
        yield return new WaitForSeconds(0.05f);
        Quaternion _rotation = transform.rotation;
        for (int i=0; i<iteration; i++)
        {
            transform.Rotate(0,90.0f/iteration,0,Space.World);
            yield return null;
        }
        transform.rotation = _rotation;
        transform.Rotate(0,90,0,Space.World);
        Revolve(ref point[FrontLeft], ref point[FrontRight], ref point[BackRight], ref point[BackLeft]);
    }
    IEnumerator OnGameOver()
    {
        Debug.Log("Game Over");
        endFreeze = true;
        int iteration = 300;
        soundManager.PlaySound("fail");
        for (int i = 0; i < iteration; i++)
        {
            transform.position += new Vector3(0,-8.0f/iteration,0);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(PuzzleManager.nameOfMainScene);
    }
    IEnumerator PuzzleSolved()
    {

        endFreeze = true;
        if (map.currentPuzzle == 0)
        {
            soundManager.PlaySound("Title");
            title.TextUp();
            while (!title.isFinish)
            {
                yield return null;
            }
            yield return new WaitForSeconds(6f);
            map.nextPuzzle();
        }
        else
        {
            soundManager.PlaySound("puzzleComplete");
            yield return new WaitForSeconds(4f);
            map.nextPuzzle();
        }
    }
}
