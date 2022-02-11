﻿using FluentValidation;
using MG.LeaveManagement.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Dtos.LeaveRequest.Validators
{
    public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveTypeRepository;

        public UpdateLeaveRequestDtoValidator(ILeaveRequestRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
           

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
