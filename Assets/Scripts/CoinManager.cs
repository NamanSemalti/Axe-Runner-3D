using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinManager : MonoBehaviour
{

    public int Coin;
    public Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        Coin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = Coin.ToString();
    }
}
