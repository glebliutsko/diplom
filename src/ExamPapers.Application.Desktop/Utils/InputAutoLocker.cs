using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Input;

namespace ExamPapers.Application.Desktop.Utils;

public class InputAutoLocker : IDisposable
{
    public bool IsLocked { get; private set; }
    private readonly IEnumerable<InputStateKeeper> _states;

    protected InputAutoLocker(IEnumerable<InputStateKeeper> states)
    {
        _states = states;
    }

    public void LockAll()
    {
        if (IsLocked)
            return;

        foreach (var state in _states)
            state.Lock();

        IsLocked = true;
    }

    public void UnlockAll()
    {
        if (!IsLocked)
            return;

        foreach (var state in _states)
            state.Unlock();

        IsLocked = false;
    }

    public void Dispose()
    {
        UnlockAll();
    }

    public static InputAutoLocker Lock(params InputElement[] elements)
    {
        var locker = new InputAutoLocker(elements.Select(x => new InputStateKeeper(x)).ToArray());
        locker.LockAll();
        
        return locker;
    }

    protected class InputStateKeeper
    {
        private readonly InputElement _element;

        private readonly bool _oldValue;

        public InputStateKeeper(InputElement element)
        {
            _element = element;
            _oldValue = element.IsEnabled;
        }

        public void Lock()
        {
            _element.IsEnabled = false;
        }

        public void Unlock()
        {
            _element.IsEnabled = _oldValue;
        }
    }
}