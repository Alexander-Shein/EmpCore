using EmpCore.Domain;

namespace EmpCore.Application.ApplicationFailures
{
    public class ResourseNotFoundFailure : Failure
    {
        private const string ErrorCode = "resourse_not_found";
        private const string ErrorMessage = "Resourse not found.";

        public static readonly ResourseNotFoundFailure Instance = new();

        private ResourseNotFoundFailure() : base(ErrorCode, ErrorMessage) { }
    }
}
