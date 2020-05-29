using System.Collections.Generic;

namespace BookingService.TgBot.StateMachine
{
    public enum UserState
    {
        None,
        InMainMenu,
        InReservations,
        InFlightsMenu, 
        DepartureCountryInputStarted,
        DepartureCountryInputEnded,
        ArrivalCountryInputStarted,
        ArrivalCountryInputEnded
    }

    // TODO: refactor transitions
    public sealed class UserStateMachine : StateMachineBase<UserState>
    {
        public UserStateMachine()
        {
            CurrentState = UserState.None;

            transitions = new Dictionary<StateTransition, UserState>
            {
                { 
                    new StateTransition(UserState.None, UserState.InMainMenu), 
                    UserState.InMainMenu     
                },
                {
                    new StateTransition(UserState.InMainMenu, UserState.InMainMenu),
                    UserState.InMainMenu
                },
                { 
                    new StateTransition(UserState.InMainMenu, UserState.InReservations), 
                    UserState.InReservations
                },
                {
                    new StateTransition(UserState.InReservations, UserState.InMainMenu),
                    UserState.InMainMenu
                },
                { 
                    new StateTransition(UserState.InMainMenu, UserState.InFlightsMenu),
                    UserState.InFlightsMenu
                },
                {
                    new StateTransition(UserState.InFlightsMenu, UserState.InMainMenu),
                    UserState.InMainMenu
                },
                {
                    new StateTransition(UserState.InFlightsMenu, UserState.DepartureCountryInputStarted),
                    UserState.DepartureCountryInputStarted
                },
                {
                    new StateTransition(UserState.DepartureCountryInputStarted, UserState.DepartureCountryInputEnded),
                    UserState.DepartureCountryInputEnded
                },
                {
                    new StateTransition(UserState.DepartureCountryInputEnded, UserState.InMainMenu),
                    UserState.InMainMenu
                },
                {
                    new StateTransition(UserState.InFlightsMenu, UserState.ArrivalCountryInputStarted),
                    UserState.ArrivalCountryInputStarted
                },
                {
                    new StateTransition(UserState.ArrivalCountryInputStarted, UserState.ArrivalCountryInputEnded),
                    UserState.ArrivalCountryInputEnded
                },
                {
                    new StateTransition(UserState.ArrivalCountryInputEnded, UserState.InMainMenu),
                    UserState.InMainMenu
                }
            };
        }
    }
}