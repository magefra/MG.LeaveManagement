﻿using FluentValidation;
using MG.LeaveManagement.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
