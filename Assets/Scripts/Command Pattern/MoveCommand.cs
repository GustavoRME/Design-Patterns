using CommandPattern;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

namespace CommandPattern
{
    public class MoveCommand : ICommand
    {
        const int POSITION_LENGTH = 5;
        
        int _currentIndex;                

        Vector3[] _positions;

        public MoveCommand()
        {          
            _positions = new Vector3[POSITION_LENGTH];            
        }

        public void Execute(Transform actor)
        {
            Vector3 newPos = _positions[_currentIndex] + Vector3.forward;

            if (_currentIndex >= POSITION_LENGTH - 1)
                ShiftLeft();
            
            _currentIndex = Mathf.Clamp(_currentIndex + 1, 0, POSITION_LENGTH - 1);

            _positions[_currentIndex] = newPos;

            actor.position = newPos;

            Debug.Log("Current index after finish Execute " + _currentIndex);
        }

        public void Undo(Transform actor)
        {
            _currentIndex = Mathf.Clamp(_currentIndex - 1, 0, POSITION_LENGTH - 1);

            actor.position = _positions[_currentIndex];

            Debug.Log("Current index after finish Undo " + _currentIndex);
        }

        public void Rendo(Transform actor)
        {
            _currentIndex = Mathf.Clamp(_currentIndex + 1, 0, POSITION_LENGTH - 1);

            actor.position = _positions[_currentIndex];

            Debug.Log("Current index after finish Rendo " + _currentIndex);
        }

        private void ShiftLeft()
        {            
            for (int i = 0; i < _positions.Length - 1; i++)
            {
                _positions[i] = _positions[i + 1];
            }
        }
    }
}
