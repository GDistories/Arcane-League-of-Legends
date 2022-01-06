using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private int startCoin = 0;
    [SerializeField] private Text coinQuantity;
    public static int currentCoinQuantity;
    
    // Start is called before the first frame update
    void Start()
    {
        currentCoinQuantity = startCoin;
    }

    // Update is called once per frame
    void Update()
    {
        coinQuantity.text = currentCoinQuantity.ToString();
    }
}
