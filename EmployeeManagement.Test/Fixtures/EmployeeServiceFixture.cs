using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Test.Services;

namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeeServiceFixture : IDisposable
    {
        public IEmployeeManagementRepository EmployeeManagementRepository { get; }
        public EmployeeService EmployeeService { get; }
        public EmployeeServiceFixture()
        {
            EmployeeManagementRepository = new EmployeeManagementTestDataRepository();
            EmployeeService = new EmployeeService(EmployeeManagementRepository, new EmployeeFactory());
        }
        public void Dispose()
        {

            // add clean-up logic here
            throw new NotImplementedException();
        }

    }
}
