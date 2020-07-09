using CommandPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CommandPattern
{
    public class InputHandler
    {
        public enum Input
        {
            None,
            Movement,
            Jump,
            Shoot,
            Undo,
            Rendo
        }
        public Input input;

        InputMaster _inputActions;
        MoveCommand _moveCommand;
        JumpCommand _jumpCommand;

        public InputHandler()
        {
            _moveCommand = new MoveCommand();
            _jumpCommand = new JumpCommand();
            
            _inputActions = new InputMaster();
            
            _inputActions.Player.Movement.performed += ctx => input = Input.Movement;
            _inputActions.Player.Jump.performed += ctx => input = Input.Jump;
            _inputActions.Player.Undo.performed += ctx => input = Input.Undo;
            _inputActions.Player.Rendo.performed += ctx => input = Input.Rendo;

            _inputActions.Enable();
        }

        ~InputHandler() => _inputActions.Disable();

        public ICommand HandleInput()
        {
            ICommand command = null;

            if (input == Input.Movement)
                command = _moveCommand;
            else if (input == Input.Jump)
                command = _jumpCommand;
            else if (input == Input.Shoot)
                Debug.Log("Shoot command");

            input = Input.None;
            return command;
        }

        public void Undo(Transform actor)
        {
            _moveCommand.Undo(actor);
            //_jumpCommand.Undo(actor);
        }

        public void Rendo(Transform actor)
        {
            _moveCommand.Rendo(actor);
            //_jumpCommand.Rendo(actor);
        }
        
    }
}
