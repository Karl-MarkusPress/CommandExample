using System;
using System.Collections.Generic;

// 1. Command Interface
interface ICommand
{
    void Execute();
    void Undo();
}

// 2. Receiver
class Light
{
    public void On()
    {
        Console.WriteLine("The light is ON.");
    }

    public void Off()
    {
        Console.WriteLine("The light is OFF.");
    }
}

// 3. Concrete Command for turning light ON
class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.On();
    }

    public void Undo()
    {
        _light.Off();
    }
}

// 4. Concrete Command for turning light OFF
class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.Off();
    }

    public void Undo()
    {
        _light.On();
    }
}

// 5. Invoker
class RemoteControl
{
    private ICommand _command;
    private Stack<ICommand> _commandHistory = new Stack<ICommand>();

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
        _commandHistory.Push(_command);
    }

    public void PressUndo()
    {
        if (_commandHistory.Count > 0)
        {
            ICommand lastCommand = _commandHistory.Pop();
            lastCommand.Undo();
        }
        else
        {
            Console.WriteLine("No command to undo.");
        }
    }
}

// 6. Client
class Program
{
    static void Main()
    {
        // Create the receiver
        Light livingRoomLight = new Light();

        // Create commands
        ICommand lightOn = new LightOnCommand(livingRoomLight);
        ICommand lightOff = new LightOffCommand(livingRoomLight);

        // Create invoker and set commands
        RemoteControl remote = new RemoteControl();

        // Execute and undo commands
        remote.SetCommand(lightOn);
        remote.PressButton();  // The light is ON.
        remote.PressUndo();    // The light is OFF.

        remote.SetCommand(lightOff);
        remote.PressButton();  // The light is OFF.
        remote.PressUndo();    // The light is ON.
    }
}
