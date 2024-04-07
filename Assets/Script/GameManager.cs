using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Constants
    private static readonly string KEY_HIGHEST_SCORE = "HighetScore";
    public bool isGameOver { get; private set; }
    [Header("Audio")]

    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource GameOverSFX;

    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private int highestScore;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        score = 0;
        highestScore = PlayerPrefs.GetInt(KEY_HIGHEST_SCORE, getHighestScore());
    }

    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime;
        }

        if (getScore() > getHighestScore())
        {
            highestScore = getScore();
        }
    }
    public int getScore()
    {
        return (int)Mathf.Floor(score);
    }
    public int getHighestScore()
    {
        return highestScore;
    }
    public void GameEnd()
    {
        if (isGameOver) return;

        // Set Flag
        isGameOver = true;
        //Stop music
        musicPlayer.Stop();
        //Play SFX
        GameOverSFX.Play();
        // Save highestScore
        PlayerPrefs.SetInt(KEY_HIGHEST_SCORE, getHighestScore());

        // Reload Scene
        StartCoroutine(ReloadScene(5f));

    }
    private IEnumerator ReloadScene(float delay)
    {
        yield return new WaitForSeconds(delay);

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
