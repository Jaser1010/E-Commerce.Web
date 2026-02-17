using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.CommonResult
{
	public enum ErrorType
	{
		Failure =0,
		Validation =1,
		NotFound =2,
		Unauthorized =3,
		Forbidden =4,
		InvalidCrendentials =5,
	}
}
