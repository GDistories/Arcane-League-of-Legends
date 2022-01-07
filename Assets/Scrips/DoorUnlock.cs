using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public int NeededCoinNum = 1;

    private bool isReduceing = false;

    [SerializeField] private float time = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        CoinUI.PassCoin = NeededCoinNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (CoinUI.PassCoin <= 0)
        {
            GameController.canPass = true;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isReduceing)
            {
                isReduceing = true;
                StartReduceCoin();
            }
        }
    }

    private void StartReduceCoin()
    {
        if (CoinUI.PassCoin <= 0)
        {
            isReduceing = false;
            return;
        }
        if (CoinUI.currentCoinQuantity > 0)
        {
            GiveCoin();
            Invoke("StartReduceCoin", time);
        }
        else
        {
            isReduceing = false;
        }
    }

    private void GiveCoin()
    {
        CoinUI.currentCoinQuantity -= 1;
        CoinUI.PassCoin -= 1;
        SoundMananger.PlayAudioOneShot(SoundMananger.pickCoin);
    }
}
