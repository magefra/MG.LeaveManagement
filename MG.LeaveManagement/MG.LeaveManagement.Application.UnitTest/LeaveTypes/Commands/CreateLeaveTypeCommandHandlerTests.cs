using AutoMapper;
using MG.LeaveManagement.Application.Dtos.LeaveType;
using MG.LeaveManagement.Application.Feactures.LeaveTypes.Handlers.Commands;
using MG.LeaveManagement.Application.Feactures.LeaveTypes.Requests.Commands;
using MG.LeaveManagement.Application.Profiles;
using MG.LeaveManagement.Application.Responses;
using MG.LeaveManagement.Application.UnitTest.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MG.LeaveManagement.Application.UnitTest.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<Contracts.Persistence.ILeaveTypeRepository> _mockRepo;
        private readonly CreateLeaveTypeDtp _leaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _handler;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);

            _leaveTypeDto = new CreateLeaveTypeDto
            {
                DefaultDays = 15,
                Name = "Test DTO"
            };
        }

        [Fact]
        public async Task Valid_LeaveType_Added()
        {
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            //var leaveTypes = await _leaveTypeDto.GetAll();

            //  result.ShouldBeOfType<BaseCommandResponse>();

            //    leaveTypes.Count.ShouldBe(4);
        }

        [Fact]
        public async Task InValid_LeaveType_Added()
        {
            //    _leaveTypeDto.DefaultDays = -1;

            //    var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            //    var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

            //    leaveTypes.Count.ShouldBe(3);

            //    result.ShouldBeOfType<BaseCommandResponse>();

        }
    }
}
