using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePLayUIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highestScoreLabel;

    private static readonly int SCORE_FACTORE = 10;

    // Start is called before the first frame update
    void Start()
    {
        scoreLabel.text = getScoreString();
        highestScoreLabel.text = getHighestScoreString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = GameManager.Instance.getScore().ToString();
        highestScoreLabel.text = GameManager.Instance.getHighestScore().ToString();
    }

    private string getScoreString()
    {
        return (GameManager.Instance.getScore() * SCORE_FACTORE).ToString();
    }
    private string getHighestScoreString()
    {
        return (GameManager.Instance.getHighestScore() * SCORE_FACTORE).ToString();
    }

}
