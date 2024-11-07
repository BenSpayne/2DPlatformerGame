using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameController : MonoBehaviour
{
    public static int coinsCounter;
    //public  Manager instance
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
