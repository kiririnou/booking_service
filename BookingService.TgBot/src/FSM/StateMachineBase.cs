using System.Collections.Generic;

namespace BookingService.TgBot.StateMachine
{
    // Base for Finite-state machine
    public class StateMachineBase<TState> where TState : struct, System.IConvertible
    {
        #region StateTransition implementation
        public class StateTransition
        {
            readonly TState CurrentState;
            readonly TState NextState;

            public StateTransition(TState currentState, TState nextState)
            {
                CurrentState = currentState;
                NextState = nextState;
            }

            public override int GetHashCode()
            {
                return 42 * CurrentState.GetHashCode() + 42 * NextState.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return 
                    other != null && 
                    this.CurrentState.Equals(other.CurrentState) && 
                    this.NextState.Equals(other.NextState);
            }
        }
        #endregion

        #region StateMachine implementation
        protected Dictionary<StateTransition, TState> transitions = new Dictionary<StateTransition, TState>();
        public TState CurrentState  { get; set; }
        public TState PreviousState { get; set; }

        protected TState GetNext(TState state)
        {
            StateTransition transition = new StateTransition(CurrentState, state);
            TState nextState;
            if (!transitions.TryGetValue(transition, out nextState))
                throw new System.Exception($"Invalid transition: {CurrentState} -> {state}");
            return nextState;
        }

        public TState SetState(TState state)
        {
            PreviousState = CurrentState;
            CurrentState = GetNext(state);
            return CurrentState;
        }
        #endregion
    }
}