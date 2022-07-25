using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle
{
    public string title;
    public int col,row;
    public Vector2 startPos;
    public int[,] map;// = new int[20,20];
    Puzzle() {

    }
    public Puzzle(string Title, int Row, int Col, int[,] Map) {
        title = Title;
        row = Row; col = Col; startPos = new Vector2(0,0);
        map = Map;
        /*map = new int[Row,Col];
        for (int i=0; i<Row; i++)
            for (int j=0; j<Col; j++)
                map[i,j] = Map[i,j];
    */
    }
    public Puzzle(string Title, int Row, int Col, Vector2 StartPos, int[,] Map) {
        title = Title;
        row = Row; col = Col; startPos = StartPos;
        map = Map;
        /*map = new int[Row,Col];
        for (int i=0; i<Row; i++)
            for (int j=0; j<Col; j++)
                map[i,j] = Map[i,j];
    */
    }
}
