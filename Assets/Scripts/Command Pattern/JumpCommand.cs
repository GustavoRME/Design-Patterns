using CommandPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    const int POSITION_LENGTH = 5;

    int _currentIndex;

    Vector3[] _positions;

    public JumpCommand()
    {
        _positions = new Vector3[POSITION_LENGTH];
    }

    public void Execute(Transform actor)
    {
        Vector3 newHeigth = _positions[_currentIndex] + Vector3.up;

        if (_currentIndex >= POSITION_LENGTH - 1)
            ShiftLeft();

        _currentIndex = Mathf.Clamp(_currentIndex + 1, 0, POSITION_LENGTH - 1);

        _positions[_currentIndex] = newHeigth;

        actor.position = newHeigth;
    }

    public void Rendo(Transform actor)
    {
        _currentIndex = Mathf.Clamp(_currentIndex - 1, 0, POSITION_LENGTH - 1);

        actor.position = _positions[_currentIndex];
    }

    public void Undo(Transform actor)
    {
        _currentIndex = Mathf.Clamp(_currentIndex + 1, 0, POSITION_LENGTH - 1);

        actor.position = _positions[_currentIndex];
    }

    private void ShiftLeft()
    {
        for (int i = 0; i < _positions.Length - 1; i++)
        {
            _positions[i] = _positions[i + 1];
        }
    }
}
