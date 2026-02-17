using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.CommonResult
{
	public class Error
	{
		public string Code { get; }
		public string Description { get; }
		public ErrorType Type { get; }

		private Error(string code, string description, ErrorType type)
		{
			Code = code;
			Description = description;
			Type = type;
		}


		#region Static Fuctory Methods
		// Static Fuctory Methods To Create the Error
		public static Error Failure(string code = "General.Failure", string description = "A General Failer Error Has Occurred")
		{
			return new Error(code, description, ErrorType.Failure);
		}
		public static Error Validation(string code = "General.Validation", string description = "A General Validation Error Has Occurred")
		{
			return new Error(code, description, ErrorType.Validation);
		}
		public static Error NotFound(string code = "General.NotFound", string description = "A General No tFound Error Has Occurred")
		{
			return new Error(code, description, ErrorType.NotFound);
		}
		public static Error Unauthorized(string code = "General.Unauthorized", string description = "A General Unauthorization Error Has Occurred")
		{
			return new Error(code, description, ErrorType.Unauthorized);
		}
		public static Error Forbidden(string code = "General.Forbidden", string description = "A General Forbidden Error Has Occurred")
		{
			return new Error(code, description, ErrorType.Forbidden);
		}
		public static Error InvalidCrendentials(string code = "General.InvalidCrendentials", string description = "A General Invalid Crendentials Error Has Occurred")
		{
			return new Error(code, description, ErrorType.InvalidCrendentials);
		}
		#endregion


	}
}
