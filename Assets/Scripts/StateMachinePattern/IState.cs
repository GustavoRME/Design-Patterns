using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineFinitePattern
{

public interface IState
{
    void OnEntry();

    void Tick();

    void OnExit();
}
}
