using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application
{
    public static class ApplicationServiceRegistration
    {

        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
           services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
