using ProductDesktopApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductDesktopApp
{
    /// <summary>
    /// Interaction logic for NewProductWindow.xaml
    /// </summary>
    public partial class NewProductWindow : Window
    {
        List<Category> Categories;

        List<Product> Products;
        public NewProductWindow()
        {
            InitializeComponent();

            Categories = new List<Category>();

            Products = new List<Product>();

            ReadDatabase();
            InitializeCategoryCombobox();
        }

        private void InitializeCategoryCombobox()
        {
            categoryComboBox.ItemsSource = Categories; 
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                connection.CreateTable<Product>();
                connection.CreateTable<Category>();



                var productToSave = new Product();
                productToSave.Name = nameTextBox.Text;
                productToSave.Price = Convert.ToDouble(priceTextBox.Text);
                productToSave.CategoryId = connection.Table<Category>().ToList().First(c => c.Name == ((Category)categoryComboBox.SelectedItem).Name).Id;
                productToSave.Description = descriptionTextBlock.Text;

                connection.Insert(productToSave);
            }

            Close();
        }

        private void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                connection.CreateTable<Category>();
                connection.CreateTable<Product>();

                Categories = connection.Table<Category>().ToList();
                Products = connection.Table<Product>().ToList();
            }
        }

        private void addCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.ShowDialog();

            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                connection.CreateTable<Category>();

                Categories = connection.Table<Category>().ToList();
            }

            InitializeCategoryCombobox();
        }
    }
}
