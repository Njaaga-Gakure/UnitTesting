using AutoMapper;
using EmployeeManagement.Controllers;
using EmployeeManagement.MapperProfiles;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net; 

namespace EmployeeManagement.Test
{
    public class StatisticsControllerTests
    {   
        [Fact]  
        public void GetStistics_InputFromHttpConnectionFeture_MustReturnInpuettedIps()
        {
            var localIpAddress = IPAddress.Parse("111.111.111.111");
            var localPort = 500; 
            var remoteIpAddress = IPAddress.Parse("222.222.222.222");
            var remotePort = 8080;

            var featureCollectionMock = new Mock<IFeatureCollection>();

            featureCollectionMock.Setup(featureCollection => featureCollection.Get<IHttpConnectionFeature>()).Returns(new HttpConnectionFeature { LocalIpAddress = localIpAddress, LocalPort = localPort, RemoteIpAddress = remoteIpAddress, RemotePort = remotePort });
            var httpContextMock = new Mock<HttpContext>(); 
            httpContextMock.Setup(httpContext => httpContext.Features).Returns(featureCollectionMock.Object);
            var mapperConfiguration = new MapperConfiguration(configuration => configuration.AddProfile<StatisticsProfile>());
            var mapper = new Mapper(mapperConfiguration);
            var statisticsController = new StatisticsController(mapper);
            statisticsController.ControllerContext = new ControllerContext() { 
                HttpContext = httpContextMock.Object,
            }; 
            var result = statisticsController.GetStatistics();  
            var actionResult = Assert.IsType<ActionResult<StatisticsDto>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var statisticsDto = Assert.IsType<StatisticsDto>(okObjectResult.Value);

            Assert.Equal(localIpAddress.ToString(), statisticsDto.LocalIpAddress);
            Assert.Equal(localPort, statisticsDto.LocalPort);
            Assert.Equal(remoteIpAddress.ToString(), statisticsDto.RemoteIpAddress);
            Assert.Equal(remotePort, statisticsDto.RemotePort);



        }
    }
}
