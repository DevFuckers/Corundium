using System.Collections.Generic;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.Cursor
{
	public class CursorService : ICursorService
	{
		readonly List<string> _reasons;

		public CursorService()
		{
			_reasons = new List<string>();
		}

		public void AddReasonToHide(string id)
		{
			if (_reasons == null)
				return;

			if (_reasons.Contains(id))
				return;

			_reasons.Add(id);
			CheckReasonsToShowCursor();
		}

		public void RemoveReasonToHide(string id)
		{
			if (_reasons == null)
				return;

			if (_reasons.Contains(id))
			{
				_reasons.Remove(id);
				CheckReasonsToShowCursor();
			}
		}

		void CheckReasonsToShowCursor()
		{
			if (_reasons == null)
				return;

			if (_reasons.Count == 0)
				SetCursorVisibility(true);
			else
				SetCursorVisibility(false);
		}

		void SetCursorVisibility(bool visible)
		{
			UnityEngine.Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
			UnityEngine.Cursor.visible = visible;
		}
	}
}