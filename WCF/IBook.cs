using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IBookCallback))]
    public interface IBook
    {
        [OperationContract]
        Book[] GetAll();
        [OperationContract]
        Book[] GetByAuthor(string author);
        [OperationContract]
        void Add(Book newBook);
        [OperationContract]
        void Modify(int id, Book newBook);
        [OperationContract]
        void Delete(int id);
        [OperationContract(IsOneWay = true)]
        void Contains(Book book);
    }

    public interface IBookCallback
    {
        [OperationContract(IsOneWay = true)]
        void ContainsResult(bool result);
    }

    [DataContract]
    public class Book
    {
        [DataMember(IsRequired = false)]
        public int? Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public float Score { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public DateTime ReleaseDate { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is Book)
            {
                Book book = obj as Book;
                if (book.ReleaseDate == ReleaseDate &&
                        book.Title == Title &&
                        book.Score == Score &&
                        book.Description == Description &&
                        book.Author == Author)
                    if (book.Id != null && book.Id == Id)
                        return true;
                    else if (book.Id == null)
                        return true;
            }
            return false;
        }
    }
}
