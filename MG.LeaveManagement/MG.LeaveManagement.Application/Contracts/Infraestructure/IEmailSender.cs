using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Contracts.Infraestructure
{
    public class IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }

}
