namespace Castles
{
    public class EventManager
    {
        public delegate void TurnDelegate();
        public delegate void PauseDelegate();

        public event TurnDelegate OnTurnEnd;
        public event TurnDelegate OnTurnStart;
        public event PauseDelegate OnPause;
        public event PauseDelegate OnUnPause;

        public void TurnEndInvoke()
        {
            if (OnTurnEnd != null)
                OnTurnEnd();
        }

        public void TurnStartInvoke()
        {
            if (OnTurnStart != null)
                OnTurnStart();
        }

        public void PauseInvoke()
        {
            if (OnPause != null)
                OnPause();
        }

        public void UnPauseInvoke()
        {
            if (OnUnPause != null)
                OnUnPause();
        }
    }
}
