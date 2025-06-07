using book_manager_wpf.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_manager_wpf.Services
{
    public class BookService
    {
        private SQLiteAsyncConnection _db;

        private async Task Init()
        {
            if (_db != null)
                return;

            _db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _db.CreateTableAsync<Book>();
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            await Init();
            return await _db.Table<Book>().ToListAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            await Init();
            return await _db.Table<Book>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveBookAsync(Book book)
        {
            await Init();
            if (book.Id != 0)
            {
                return await _db.UpdateAsync(book);
            }
            else
            {
                return await _db.InsertAsync(book);
            }
        }

        public async Task<int> DeleteBookAsync(Book book)
        {
            await Init();
            return await _db.DeleteAsync(book);
        }
    }
}