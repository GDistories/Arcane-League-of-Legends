using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPass : MonoBehaviour
{
    public int sceneNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if (Input.GetKeyDown(KeyCode.O))
        // {
            if (GameController.canPass)
            {
                SceneManager.LoadScene(sceneNum);
            }
        // }
    }
}
