using System;
using System.Collections.Generic;

public class PauseService : IPauseService
{
    private List<IPausable> _stoppables = new();

    public void Add(IPausable pausable) 
        => _stoppables?.Add(pausable);

    public void Remove(IPausable pausable) 
        => _stoppables?.Remove(pausable);

    public void PerformStop()
    {
        foreach (IPausable item in _stoppables) 
            item.Stop();
    }

    public void PerformResume()
    {
        foreach (IPausable item in _stoppables) 
            item.Resume();
    }

    public void Dispose()
    {
        _stoppables.Clear();
        _stoppables = null;
    }
}
