namespace SpriteFlight
{
    public class ScoreModel
    {
        private int score;
        public int Score
        {
            get => score;
            private set => score = value;
        }
        public void AddPoints(int amount)
        {
            Score += amount;
        }
    }
}
