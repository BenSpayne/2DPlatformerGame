using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Manager : MonoBehaviour
{
    public int coinsCounter;
    public static Manager instance;
    [SerializeField] Animator animator;
        public TextMeshProUGUI coinText;

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Start");
    }

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
