using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Manager : MonoBehaviour
{
    public int coinsCounter = 0;
        public TextMeshProUGUI coinText;

        void Update()
        {
            coinText.text = coinsCounter.ToString();
        }

        
}
