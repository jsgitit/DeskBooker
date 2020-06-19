using DeskBooker.Core.Domain;
using System;
using Xunit;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessorTests
    {
        private readonly DeskBookingRequestProcessor _processor;

        public DeskBookingRequestProcessorTests()
        {
            _processor = new DeskBookingRequestProcessor();
        }

        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            //arrange
            var request = new DeskBookingRequest
            {
                FirstName = "Jon",
                LastName = "Smart",
                Email = "bogusemail@home.com",
                Date = new DateTime(2020, 10, 1)
            };

            //act
            DeskBookingResult result = _processor.BookDesk(request);

            //assert
            Assert.NotNull(result);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);

        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            // arrange
            // uses processor field above
            
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));
            
            // assert
            Assert.Equal("request", exception.ParamName);

        }
    }
}
