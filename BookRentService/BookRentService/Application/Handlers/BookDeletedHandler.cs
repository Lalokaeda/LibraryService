using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.Commands;
using BookRentService.Domain.Entities;
using BookRentService.Domain.Interfaces;
using MediatR;

namespace BookRentService.Application.Handlers
{
    public class BookDeletedHandler : IRequestHandler<DeleteRentByBookIdCommand, bool>
    {
        private readonly IBaseRepository<BookRent> _bookRentRepository;
        private readonly IBaseRepository<BookExemplarRent> _bookExemplarRentRepository;

        public BookDeletedHandler(IBaseRepository<BookRent> bookRentRepository, IBaseRepository<BookExemplarRent> bookExemplarRentRepository)
        {
            _bookRentRepository = bookRentRepository;
            _bookExemplarRentRepository = bookExemplarRentRepository;
        }

        public async Task<bool> Handle(DeleteRentByBookIdCommand request, CancellationToken cancellationToken)
        {
            var exemplar_rents = await _bookExemplarRentRepository.GetByConditionAsync(x => x.BookExemplarId == request.BookId);
            List<int> rentsId = new List<int>();
            if (exemplar_rents != null && exemplar_rents.Count() > 0)
            {
                foreach (var i in exemplar_rents)
                {
                    if (!rentsId.Contains(i.BookRentId))
                    {
                        rentsId.Add(i.BookRentId);
                    }
                }

                await _bookRentRepository.DeleteRangeAsync(rentsId.ToArray());
            }
            return true;
        }
    }
}