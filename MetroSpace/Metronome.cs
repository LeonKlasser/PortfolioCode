using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[System.Serializable]
public class ScoreToBpm
{
    public int Score;
    public int BPM;
}

public class Metronome : MonoBehaviour
{
    [SerializeField] private AudioClip metronomeSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private List<ScoreToBpm> scoreToBpmList;
    [FormerlySerializedAs("scoreForNextLevels")] [SerializeField] private int scoreForNextLevel;

    public int bpm = 10; 

    private float beatInterval;
    private Score score;
    private float nextBeatTime;

    private void Start()
    {
        CalculateBeatInterval();
        score = FindObjectOfType<Score>();
        nextBeatTime = Time.time + beatInterval;
    }

    private void Update()
    {
        foreach (ScoreToBpm scoreToBpm in scoreToBpmList)
        {
            if (score.scoreAmount >= scoreToBpm.Score)
            {
                bpm = scoreToBpm.BPM;

                break;
            }
        }

        if (score.scoreAmount >= scoreForNextLevel)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (Time.time >= nextBeatTime)
        {
            audioSource.PlayOneShot(metronomeSound);

            nextBeatTime += beatInterval;
            
            enemySpawner.SpawnEnemy();
        }

        if (bpm != Mathf.RoundToInt(60f / beatInterval))
        {
            CalculateBeatInterval();
        }
    }

    private void CalculateBeatInterval()
    {
        beatInterval = 60f / bpm;
    }
}
