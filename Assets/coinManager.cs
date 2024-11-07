using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class coinManager : MonoBehaviour
{
    public int coinsCounter;
    public static coinManager instance;
    public TextMeshProUGUI coinText;

        void Start()
        {

            coinText.text = coinsCounter.ToString();
            

        }

        private void OnGUI()
        {
            coinText.text = coinsCounter.ToString();
        }


        public void ChangeCoin(int amount)
        {
            coinsCounter += amount;
        }

        
}
