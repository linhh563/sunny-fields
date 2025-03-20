namespace Management
{
    public enum CharacterCommand
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        DoNothing
    }

    public enum CharacterMovementState
    {
        Idle,
        Moving,
        Running,
        Strolling
    }

    public enum CharacterDirection 
    {
        Up,
        Down,
        Left,
        Right
    }
}