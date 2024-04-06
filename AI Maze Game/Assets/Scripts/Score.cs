using UnityEngine;
using UnityEngine.UI;

public class Score : GameManager
{
    public Transform player;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ("score");
    }
}
