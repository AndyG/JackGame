using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM2
{
  //Base class for all states, only the required methods need to be overriden
  public abstract class StateBase
  {
    [HideInInspector] public FSM2 fsm;

    public virtual void Update() { }
    public virtual void Exit() { }
    public virtual void OnMessage(string message) { }
    public abstract string GetAnimation();
  };

  public abstract class State : StateBase { public virtual void Enter() { } }
  public abstract class State1Param<T> : StateBase { public abstract void Enter(T p); }
  public abstract class State2Params<T0, T1> : StateBase { public abstract void Enter(T0 p0, T1 p1); }
  public abstract class State3Params<T0, T1, T2> : StateBase { public abstract void Enter(T0 p0, T1 p1, T2 p2); }

  //Current state being handled in this FSM
  public StateBase currentState { private set; get; }

  bool ChangeStateBase(StateBase _newState)
  {
    //Exit the current state
    if (currentState != null)
    {
      currentState.Exit();
    }

    //Change to the new state
    currentState = _newState;

    if (_newState != null)
    {
      _newState.fsm = this;
      return true;
    }
    return false;
  }

  public void ChangeState(State _newState)
  {
    if (ChangeStateBase(_newState)) _newState.Enter();
  }

  public void ChangeState<T>(State1Param<T> _newState, T p)
  {
    if (ChangeStateBase(_newState)) _newState.Enter(p);
  }

  public void ChangeState<T0, T1>(State2Params<T0, T1> _newState, T0 p0, T1 p1)
  {
    if (ChangeStateBase(_newState)) _newState.Enter(p0, p1);
  }

  public void ChangeState<T0, T1, T2>(State3Params<T0, T1, T2> _newState, T0 p0, T1 p1, T2 p2)
  {
    if (ChangeStateBase(_newState)) _newState.Enter(p0, p1, p2);
  }

  public void UpdateCurrentState()
  {
    if (currentState != null)
    {
      currentState.Update();
    }
  }

}