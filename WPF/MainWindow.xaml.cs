using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.BooksReference;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public bool ModifyMode {
            get { return _modifyMode; }
            set { _modifyMode = value; if (_modifyMode) { FormButtonString = "Modify"; } else { FormButtonString = "Add"; } } 
        }
        bool _modifyMode;
        public string FormButtonString
        {
            get { return _formButtonString; }
            set { _formButtonString = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FormButtonString))); }
        }
        string _formButtonString;

        public string ContainsString
        {
            get { return _containsString; }
            set { _containsString = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContainsString))); }
        }
        string _containsString;

        public BookClient BookClient;
        public Book[] Books;
        public Book SelectedBook;

        public MainWindow()
        {
            ContainsString = "?";
            InitializeComponent();
            DataContext = this;
            ModifyMode = false;
            InstanceContext context = new InstanceContext(new Callback(this));
            BookClient = new BookClient(context);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (!ModifyMode)
            {
                ModifyMode = true;
                SelectedBook = (Book)((Button)sender).Tag;
                txbAuthor.Text = SelectedBook.Author;
                txbDescription.Text = SelectedBook.Description;
                dpDate.SelectedDate = SelectedBook.ReleaseDate;
                txbScore.Text = SelectedBook.Score.ToString();
                txbTitle.Text = SelectedBook.Title;
            }
            else
            {
                ModifyMode = false;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Books = BookClient.GetByAuthor(txbSearchAuthor.Text);
            lvItems.ItemsSource = Books;
        }

        private void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            Books = BookClient.GetAll();
            lvItems.ItemsSource = Books;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            BookClient.Delete(int.Parse(txbId.Text));
        }

        private void btnAddModify_Click(object sender, RoutedEventArgs e)
        {
            if (ModifyMode)
            {
                BookClient.Modify(SelectedBook.Id.Value, ConstructBook());
            }
            else
            {
                BookClient.Add(ConstructBook());
            }
        }

        private Book ConstructBook()
        {
            return new Book()
            {
                Author = txbAuthor.Text,
                Description = txbDescription.Text,
                ReleaseDate = dpDate.SelectedDate.Value,
                Score = float.Parse(txbScore.Text),
                Title = txbTitle.Text
            };
        }

        private void btnCheckContains_Click(object sender, RoutedEventArgs e)
        {
            BookClient.Contains(ConstructBook());
        }
    }
}
