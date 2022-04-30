using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WCF
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class BookService : IBook
    {
        Dictionary<int, Book> Books { get; set; }
        IBookCallback Callback { get; set; }

        BookService()
        {
            Books = new Dictionary<int, Book>();
            Callback = OperationContext.Current.GetCallbackChannel<IBookCallback>();

        }
        public void Add(Book newBook)
        {
            newBook.Id = Books.Keys.LastOrDefault() + 1;
            Books.Add(newBook.Id.Value, newBook);
        }

        public void Delete(int id)
        {
            if (Books.ContainsKey(id))
            {
                Books.Remove(id);
            }
        }

        public Book[] GetAll()
        {
            return Books.Values.ToArray();
        }

        public void Modify(int id, Book newBook)
        {
            newBook.Id = id;
            Books[id] = newBook;
        }

        public void Contains(Book book)
        {
            Thread.Sleep(1000);
            Callback.ContainsResult(Books.ContainsValue(book));
        }

        public Book[] GetByAuthor(string author)
        {
            return Books.Where(b => b.Value.Author == author).Select(b => b.Value).ToArray();
        }
    }
}
