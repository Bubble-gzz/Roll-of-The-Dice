using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMusic : SoundManager
{
    // Start is called before the first frame updata

    // Update is called once per frame
    protected override void Start()
    {
        audioNames = new string[]{"bgm-Rain"};
        base.Start();
        audioSrc.clip = clips["bgm-Rain"];
        //audioSrc.Play();
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(PuzzleManager.nameOfMainScene);
    }
    void Update()
    {
        
    }
}
