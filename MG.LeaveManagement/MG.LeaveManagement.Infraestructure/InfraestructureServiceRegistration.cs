using MG.LeaveManagement.Application.Contracts.Infraestructure;
using MG.LeaveManagement.Application.Models;
using MG.LeaveManagement.Infraestructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MG.LeaveManagement.Infraestructure
{
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
