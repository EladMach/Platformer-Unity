using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI bestTimeText;
    

    private void Start()
    {
        bestTimeText.text = "Best Time: " + PlayerPrefs.GetFloat("TimeRemaining");
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
