namespace Score
{
    class UserScore
    {
        private static string username;
        private static string scoremin;
        private static string scoresec;
        public UserScore(string _username, string _scoremin, string _scoresec)
        {
            username = _username;
            scoremin = _scoremin;
            scoresec = _scoresec;
        }
    }
}