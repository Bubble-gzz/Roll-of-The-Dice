using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Vanish()
    {
        StartCoroutine(Fadeout());
    }
    IEnumerator Fadeout()
    {
        Color color = GetComponent<Renderer>().material.color;
        int iteration = 200;
        for (int i = 0; i < iteration; i++)
        {
        //    Debug.Log("alpha: " + color.a.ToString());
            color.a -= 1.0f/iteration;
            GetComponent<Renderer>().material.color = color;
            yield return null;
        }

    }

}
