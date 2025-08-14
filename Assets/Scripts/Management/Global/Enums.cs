namespace Management
{
    [System.Serializable]
    public enum CharacterDirection
    {
        Up,
        Down,
        Left,
        Right
    }


    [System.Serializable]
    public enum CharacterMovementState
    {
        Idle,
        Moving,
        Running,
        Strolling
    }


    [System.Serializable]
    public enum CharacterCommand
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        DoNothing
    }


    [System.Serializable]
    public enum ObjectPoolType
    {
        Plant,
        GameObject,
        None
    }


    [System.Serializable]
    public enum GroundState
    {
        None = 1,
        Hoed = 2,
        Watered = 3
    }


    [System.Serializable]
    public enum ClotheType
    {
        Hat,
        Hair,
        Shirt,
        Pant
    }


    [System.Serializable]
    public enum GameLanguage
    {
        Vietnamese = 0,
        English = 1
    }


    [System.Serializable]
    public enum DecisionType
    {
        Talking,
        Buying,
        Selling
    }

    [System.Serializable]
    public enum FarmSize
    {
        Small = 1,
        Medium = 2, 
        Large = 3
    }
}