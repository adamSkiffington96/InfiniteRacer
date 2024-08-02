using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    private bool _addScore = true;

    private float _currentTime = 0f;
    private float _score = 0f;

    private float _highScore = 0f;

    public Text _currentScoreText;
    public Text _highScoreText;


    public void ChangeHighScore(bool enabled)
    {
        if (enabled)
        {
            _addScore = true;
        }
        else
        {
            _addScore = false;

            if (_score > _highScore) {
                _highScore = _score;
                _highScoreText.color = Color.red;
            }
            else {
                _highScoreText.color = Color.white;
            }
        }

        print("Your high score was " + _score);

        _currentTime = 0f;
        _score = 0f;
        //_currentScoreText.text = "Score: 0";
        _highScoreText.text = "High Score: " + _highScore.ToString();
    }

    void Update()
    {
        if (_addScore)
        {
            _currentTime += Time.deltaTime;

            _score = (int) _currentTime * 10;

            _currentScoreText.text = "Score: " + _score.ToString();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _addScore = true;
            }
        }
    }
}
