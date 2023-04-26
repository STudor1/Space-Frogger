using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Stack<Command> commands = new Stack<Command>();

    public void ExecuteCommand(Command command)
    {
        commands.Push(command); //add the command to the stack
        command.Execute(); //execute the command
    }

    public void UndoCommand()
    {
        if (commands.Peek() == null) return; //if stack empty we don't do anything

        commands.Peek().Undo(); //undo the last command
        commands.Pop(); //remove the command from the stack
        Debug.Log("Undid command");
    }
}
