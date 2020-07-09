using CommandPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class GameLooping : MonoBehaviour
    {
        InputHandler _inputHandler;

        // Start is called before the first frame update
        void Start()
        {
            _inputHandler = new InputHandler();
        }

        // Update is called once per frame
        void Update()
        {
            InputHandler.Input inputType = _inputHandler.input;
            
            if (inputType == InputHandler.Input.Undo)
                _inputHandler.Undo(transform);
            else if (inputType == InputHandler.Input.Rendo)
                _inputHandler.Rendo(transform);

            ICommand command = _inputHandler.HandleInput();
            
            if(command != null)
            {
                command.Execute(transform);
            }

        }
    }
}
