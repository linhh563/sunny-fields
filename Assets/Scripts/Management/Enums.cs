using System;

namespace Management
{
    [Serializable]
        public enum CharacterDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    
    public enum CharacterMovementState
    {
        Idle,
        Moving,
        Running,
        Strolling
    }

    public enum CharacterCommand
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        DoNothing
    }

    public enum ObjectPoolType
    {
        Plant,
        GameObject,
        None
    }

    public enum ClotheType
    {
        Hat,
        Hair,
        Shirt,
        Pant
    }
}