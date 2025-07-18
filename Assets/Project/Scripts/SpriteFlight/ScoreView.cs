using UnityEngine;
using TMPro;
using System;

namespace SpriteFlight
{
    public class ScoreView : MonoBehaviour
    {
        public TMP_Text ScoreText;
        private float timeElapsed;
        TimeSpan timeSpan;
        private ScorePresenter _scorePresenter;

        public void SetPresenent(ScorePresenter scorePresenter)
        {
            _scorePresenter = scorePresenter;
        }

        public void UpdateScoreDisplay(string score)
        {
            ScoreText.text = score;
        }
        private void Update()
        {

        }
    }
}
