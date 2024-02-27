using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public static int seed;
    private int newseed;
    public TMP_InputField input;

    public void LoadSeed()
    {
        bool isNum = int.TryParse(input.text, out newseed);
        if (isNum)
        {
            seed = int.Parse(input.text);
        }
        else
        {
            input.text = seed.ToString();
        }
        

    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }
    // Start is called before the first frame update
    void Start()
    {
        seed = Random.Range(-21474836, 21474836);
        input.text = seed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
