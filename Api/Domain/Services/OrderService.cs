using Domain.Models;
using Domain.Repositories;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUserBookRepository _userBookRepository;
        public OrderService(IUserBookRepository userBookRepository)
        {
            _userBookRepository = userBookRepository;
        }

        public async Task<IEnumerable<UserBook>> GetUserOrderedBooks(string userEmail)
        {
            return await _userBookRepository.GetUserBooksAsync(userEmail);
        }

        public async Task MakeOrder(IEnumerable<UserBook> orderData)
        {
            await _userBookRepository.AddRangeAsync(orderData);
        }

        public async Task UpdateBookCount(long userId, long bookId, int newCount, int oldCount)
        {
            await _userBookRepository.UpdateAsync(userId, bookId, newCount, oldCount);
        }
    }
}
