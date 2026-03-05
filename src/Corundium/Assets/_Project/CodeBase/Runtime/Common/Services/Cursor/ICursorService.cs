namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.Cursor
{
	public interface ICursorService
	{
		void AddReasonToHide(string id);
		void RemoveReasonToHide(string id);
	}
}