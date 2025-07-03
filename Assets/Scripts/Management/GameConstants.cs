namespace Management
{
    public static class AnimationName
    {
        public const string Idle = "player_idle";
        public const string IdleDown = "player_idle_down";
        public const string IdleUp = "player_idle_up";
        public const string Moving = "player_moving";
        public const string MovingDown = "player_moving_down";
        public const string MovingUp = "player_moving_up";
        public const string Run = "player_run";
        public const string RunDown = "player_run_down";
        public const string RunUp = "player_run_up";
    }

    public static class CharacterDefaultStats
    {
        public const float DefaultSpeed = 5.0f;
    }

    public static class EnvironmentConstants
    {
        // the duration in real life (second) corresponding to a day in game
        public const float DayLength = 20;
        public const int MinuteInDay = 1440;
    
    }
}