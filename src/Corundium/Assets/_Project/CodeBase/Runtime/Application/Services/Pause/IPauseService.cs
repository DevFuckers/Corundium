using System;

public interface IPauseService
{
	void Add(IPausable pausable);
	void Remove(IPausable pausable);
	void PerformResume();
	void PerformStop();
}
