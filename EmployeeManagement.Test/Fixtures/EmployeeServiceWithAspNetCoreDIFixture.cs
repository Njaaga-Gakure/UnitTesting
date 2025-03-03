using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Test.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeeServiceWithAspNetCoreDIFixture : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        public IEmployeeManagementRepository EmployeeManagementRepository { get { return _serviceProvider.GetService<IEmployeeManagementRepository>();  } }

        public IEmployeeService EmployeeService { get { return _serviceProvider.GetService<IEmployeeService>();  } }

        public EmployeeServiceWithAspNetCoreDIFixture()
        {
            var services = new ServiceCollection();
            services.AddScoped<EmployeeFactory>(); 
            services.AddScoped<IEmployeeManagementRepository, EmployeeManagementTestDataRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            _serviceProvider = services.BuildServiceProvider(); 
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
