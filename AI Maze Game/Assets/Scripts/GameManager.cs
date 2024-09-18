using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager gm;
    public int score;

    public Text subtitle;

    public Text hudScore;

    public int healthy;
    public float boostTimer;
    public float slowTimer;
    void Start()
    {
        score = 0;
        healthy = 500;
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        hudScore.text = score.ToString();

        if (healthy <= 0)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;
        }

        if (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
        }
    }
}

