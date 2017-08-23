namespace GameScreen.Character
{
    public class CharacterState
    {
        public delegate void ChangeStateHandler();

        public event ChangeStateHandler ChangeState;

        public enum StateName
        {
            DEFAULT,
            DEAD
        }

        private StateName currentState = StateName.DEFAULT;

        public StateName CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                if (ChangeState != null)
                    ChangeState();
            }
        }
    }
}
