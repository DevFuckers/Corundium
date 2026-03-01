namespace DevFuckers._Project.CodeBase.Runtime.Features.Pause
{
	public interface IPauseController
	{
		void Add(IPausable pausable);
		void Remove(IPausable pausable);
		void PerformResume();
		void PerformStop();
	}
}
