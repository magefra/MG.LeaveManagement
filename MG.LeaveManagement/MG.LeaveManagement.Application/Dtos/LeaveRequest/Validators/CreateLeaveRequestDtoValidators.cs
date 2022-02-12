using FluentValidation;
using MG.LeaveManagement.Application.Contracts.Persistence;

namespace MG.LeaveManagement.Application.Dtos.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidators : AbstractValidator<CreateLeaveRequestDto>
    {
        public CreateLeaveRequestDtoValidators(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.StartDate)
                .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue");

            RuleFor(p => p.EndDate)
                .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue");


            RuleFor(p => p.LeaveTypeId)
                .MustAsync(async (id, token) =>
               {
                   var leaveTypeExist = await leaveTypeRepository.Exists(id);
                   return !leaveTypeExist;

               }).WithMessage("{PropertyName} does not exist");
        }
    }
}
