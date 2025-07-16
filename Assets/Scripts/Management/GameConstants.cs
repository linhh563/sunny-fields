namespace Management
{
    public static class AnimationName
    {
        public const string IDLE = "player_idle";
        public const string IDLE_DOWN = "player_idle_down";
        public const string IDLE_UP = "player_idle_up";
        public const string MOVING = "player_moving";
        public const string MOVING_DOWN = "player_moving_down";
        public const string MOVING_UP = "player_moving_up";
        public const string RUN = "player_run";
        public const string RUN_DOWN = "player_run_down";
        public const string RUN_UP = "player_run_up";
    }

    public static class CharacterDefaultStats
    {
        public const float DEFAULT_SPEED = 5.0f;
    }

    public static class EnvironmentConstants
    {
        // the duration in real life (second) corresponding to a day in game
        public const float DAY_LENGTH = 20;
        public const int MINUTES_IN_DAY = 1440;
    
    }
}