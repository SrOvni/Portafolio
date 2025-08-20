using System;
using System.Runtime.Serialization;
using Codice.Client.Common;
using UtilitiesLibrary.ImprovedTimers;

namespace SpriteFlight
{
    public class ScorePresenter
    {
        ScoreModel scoreModel;
        ScoreView scoreView;
        private float timer;
        private float interval = 1f;
        TimeSpan _timeSpan;
        FrequencyTimer frequencyTimer;


        public ScorePresenter(ScoreModel model, ScoreView view)
        {
            scoreModel = model;
            scoreView = view;
            scoreView.SetPresenent(this);

            //Timer
            frequencyTimer = new FrequencyTimer(1);
            frequencyTimer.OnTick += OnSecondPassed;
            frequencyTimer.Start();


            view.UpdateScoreDisplay(FormatTime(_timeSpan));
        }

        public void OnSecondPassed()
        {
            _timeSpan = _timeSpan.Add(TimeSpan.FromSeconds(1));
            scoreView.UpdateScoreDisplay(FormatTime(_timeSpan));
        }

        public string FormatTime(TimeSpan ts)
        {
            return string.Format("{0:D2}:{0:D2}", ts.Minutes, ts.Seconds);
        }

        public void Reset()
        {
            frequencyTimer.Reset();
            _timeSpan = TimeSpan.Zero;
            scoreView.UpdateScoreDisplay(FormatTime(_timeSpan));
        }
        public void Stop()
        {
            frequencyTimer?.Stop();
        }
        public void Dispose()
        {
            frequencyTimer.OnTick -= OnSecondPassed;
            frequencyTimer.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }

    }
}
