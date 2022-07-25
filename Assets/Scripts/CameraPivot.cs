using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    // Start is called before the first frame update
    MapManager map;
    Vector3 offset = new Vector3(-9,8.5f,-9);
    void Start()
    {
        map = GameObject.Find("MapManager").GetComponent<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = map.mapPivot + offset;
    }
}
