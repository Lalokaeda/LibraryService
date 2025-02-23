using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookRentService.Domain.Entities;
using BookRentService.Domain.Interfaces;
using BookRentService.Infrastructure.BackgroundJobs;
using BookRentService.Infrastructure.Service;
using Moq;

namespace BookRentService.Tests
{
    public class FineServiceTests
    {
        private readonly Mock<IFineRepository> _fineRepositoryMock;
        private readonly Mock<IBaseRepository<BookRent>> _bookRentRepositoryMock;
        private readonly FineService _fineService;

        public FineServiceTests()
        {
            _fineRepositoryMock=new Mock<IFineRepository>();
            _bookRentRepositoryMock=new Mock<IBaseRepository<BookRent>>();
            _fineService=new FineService(_fineRepositoryMock.Object, _bookRentRepositoryMock.Object);
        }

        [Fact]
        public async Task CheckAndApplyFinesAsync_RentOverdue()
        {
            var overdueRent = new BookRent
            {
                Id=1,
                EndDate=DateTime.UtcNow.AddDays(-5),
                ReturnDate=null
            };
            _bookRentRepositoryMock.Setup(x=>x.GetByConditionAsync(It.IsAny<Expression<Func<BookRent, bool>>>()))
                                    .ReturnsAsync(new List<BookRent>{overdueRent});
            _bookRentRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<BookRent>()))
                                .Returns(Task.CompletedTask);
            
            _fineRepositoryMock.Setup(x=>x.ExistsAsync(It.IsAny<Expression<Func<Fine, bool>>>())).ReturnsAsync(false);
            _fineRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Fine>()))
                                .Returns(Task.CompletedTask);

            await _fineService.CheckAndApplyFinesAsync();

            _fineRepositoryMock.Verify(x => x.AddAsync(It.Is<Fine>(
                        fine => fine.BookRentId == overdueRent.Id && fine.Amount > 0)), Times.Once);

            _bookRentRepositoryMock.Verify(x => x.UpdateAsync(It.Is<BookRent>(
                        rent => rent.Id == overdueRent.Id && rent.RentStatusId==4)), Times.Once);
        }
    }
}