using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isGameAlive = true;
    public static CameraShake camShake;
    public static bool canSickle;
    public static bool canPass = false;

    private void Start()
    {
        canPass = false;
    }
}
