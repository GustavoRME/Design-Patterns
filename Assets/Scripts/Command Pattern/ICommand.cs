using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public interface ICommand
    {        
        void Execute(Transform actor);
        void Undo(Transform actor);
        void Rendo(Transform actor);
    }
}
