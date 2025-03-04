namespace EmployeeManagement.Test.TestData
{
    public class StronlyTypedEmployeeServiceTestDataFromFile : TheoryData<int, bool>
    {
        public StronlyTypedEmployeeServiceTestDataFromFile()
        {
            var testDataLines = File.ReadAllLines("TestData/EmployeeServiceTestData.csv");
            foreach (var line in testDataLines) 
            {
                var splitString = line.Split(',');
                if (int.TryParse(splitString[0], out int raise) && bool.TryParse(splitString[1], out bool minimumRaiseGiven)) {
                    Add(raise, minimumRaiseGiven); 
                }
            }
        }
    }
}
