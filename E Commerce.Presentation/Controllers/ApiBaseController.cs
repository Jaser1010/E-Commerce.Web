using E_Commerce.Shared.CommonResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ApiBaseController : ControllerBase
	{
		// Handle Result Without Value
		// 1. If Result Is Success Return NoContent 204
		// 2. If Result Is Failure Return Problem With Statuse Code And Error Details

		protected IActionResult HandleReult(Result result)
		{
			if (result.IsSuccess) // True
				return NoContent(); // 204
			else
				return HandleProblem(result.Errors);
		}

		// Handle Result With Value 
		// 1. If Result Is Success Return Ok 200 With Value
		// 2. If Result Is Failure Return Problem With Statuse Code And Error Details
		protected ActionResult<TValue> HandleReult<TValue>(Result<TValue> result)
		{
			if (result.IsSuccess) // True
				return Ok(result.Value); // 200
			else
				return HandleProblem(result.Errors);
		}



		private ActionResult HandleProblem(IReadOnlyList<Error> errors)
		{
			// If No Errors Are Provided => Return 500 Error [ InternalServerError ]
			if (errors.Count == 0)
				return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An Unexpected Error Occured");
			// If There's Only One Error => Haandle It As A Single Error Problem
			if(errors.All(e => e.Type == ErrorType.Validation))
				return HandleValidationProblem(errors);
			// If All Errors Are Validation Errors => HandleThem As A Validation Problem
			return HandleSingleErrorProblem(errors[0]);
		}
		private ActionResult HandleSingleErrorProblem(Error error)
		{
			return Problem
			(
			title: error.Code,
			detail: error.Description,
			type: error.Type.ToString(),
			statusCode: MapErrorTypeToStatusCode(error.Type) 
			);
		}
		private static int MapErrorTypeToStatusCode(ErrorType errorType) => errorType switch
		{
			ErrorType.NotFound => StatusCodes.Status404NotFound,
			ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
			ErrorType.Forbidden => StatusCodes.Status403Forbidden,
			ErrorType.Validation => StatusCodes.Status400BadRequest,
			ErrorType.InvalidCrendentials => StatusCodes.Status401Unauthorized,
			ErrorType.Failure => StatusCodes.Status500InternalServerError,
			_ => StatusCodes.Status500InternalServerError
		};
		private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
		{
			var modelSate = new ModelStateDictionary();
			foreach (var error in errors)
				modelSate.AddModelError(error.Code, error.Description);
			return ValidationProblem(modelSate);
		}
	}
}