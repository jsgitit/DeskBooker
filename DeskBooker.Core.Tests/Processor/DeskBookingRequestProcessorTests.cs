using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Moq;
using System;
using System.Security.Cryptography;
using Xunit;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessorTests
    {
        private readonly DeskBookingRequest _request;
        private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
        private readonly DeskBookingRequestProcessor _processor;

        public DeskBookingRequestProcessorTests()
        {
            _request = new DeskBookingRequest
            {
                FirstName = "Jon",
                LastName = "Smart",
                Email = "bogusemail@home.com",
                Date = new DateTime(2020, 10, 1)
            };

            _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
            _processor = new DeskBookingRequestProcessor(_deskBookingRepositoryMock.Object);

            // Arrange a fake request object
            

        }

        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            // this method returns result with the requests values

            // Arrange
            // assumes a fake request object has been setup in the constructor.

            // Act
            DeskBookingResult result = _processor.BookDesk(_request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_request.FirstName, result.FirstName);
            Assert.Equal(_request.LastName, result.LastName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);

        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {

            // this method throws an ArgumentNullException if the requst is null.
            
            // Arrange
            // uses processor field above
            
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));
            
            // Assert
            Assert.Equal("request", exception.ParamName);

        }

        [Fact]
        public void ShouldSaveDeskBooking()
        {
            // Arrange
            DeskBooking savedDeskBooking = null;
            _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
                .Callback<DeskBooking>(deskBooking =>
                {
                    savedDeskBooking = deskBooking;
                });

            // assumes _processor is already created in constructor

            // Act
            _processor.BookDesk(_request);
            _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);

            // Assert
            Assert.NotNull(savedDeskBooking);
            Assert.Equal(_request.FirstName, savedDeskBooking.FirstName);
            Assert.Equal(_request.LastName, savedDeskBooking.LastName);
            Assert.Equal(_request.Email, savedDeskBooking.Email);
            Assert.Equal(_request.Date, savedDeskBooking.Date);

        }
    }
}
