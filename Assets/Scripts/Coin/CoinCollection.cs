using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinCollection : MonoBehaviour
{
    public int Coins;
    public Text coinText;
    void Start()
    {
        if(PlayerPrefs.HasKey("KartosPoints"))
        {
            Coins = PlayerPrefs.GetInt("KartosPoints");
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = Coins.ToString();
        PlayerPrefs.SetInt("KartosPoints", Coins);
    }
}
