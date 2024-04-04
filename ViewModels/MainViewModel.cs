using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppFinanse.Commands;
using WpfAppFinanse.Data;
using WpfAppFinanse.Models;

namespace WpfAppFinanse.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Wallet> Wallets { get; set; } = new ObservableCollection<Wallet>();
        public ObservableCollection<Currency> Currencies { get; set; } = new ObservableCollection<Currency>();

        private Wallet _selectedWallet;
        public Wallet SelectedWallet
        {
            get => _selectedWallet;
            set
            {
                if (_selectedWallet != value)
                {
                    _selectedWallet = value;
                    OnPropertyChanged(nameof(SelectedWallet));
                    // Загрузка транзакций для выбранного кошелька
                }
            }
        }

        public ICommand AddWalletCommand { get; }
        public ICommand RemoveWalletCommand { get; }
        public ICommand AddTransactionCommand { get; }
        public ICommand RemoveTransactionCommand { get; }
        private FinanceDbContext _context;
        public MainViewModel()
        {
            _context = new FinanceDbContext();
            AddWalletCommand = new RelayCommand(AddWallet);
            RemoveWalletCommand = new RelayCommand(RemoveWallet, CanRemoveWallet);
            AddTransactionCommand = new RelayCommand(AddTransaction);
            RemoveTransactionCommand = new RelayCommand(RemoveTransaction, CanRemoveTransaction);
        }

        private void AddWallet(object parameter)//добавления кошелька
        {
            var newWallet = new Wallet
            {
                Id = Guid.NewGuid(),
                Name = "Новый кошелек", // реальное значение должно быть получено от пользователя
                Balance = 0,
                CurrencyId = Guid.NewGuid() // выберите существующий ID валюты
            };

            try
            {
                _context.Wallets.Add(newWallet);
                _context.SaveChanges();
                Wallets.Add(newWallet); // Добавляем в ObservableCollection для обновления UI
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении кошелька: {ex.Message}");
            }
        }
    

        private void RemoveWallet(object parameter)
        {
            if (SelectedWallet == null) return;

            try
            {
                _context.Wallets.Remove(SelectedWallet);
                _context.SaveChanges();
                Wallets.Remove(SelectedWallet);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении кошелька: {ex.Message}");
            }
        }

        private bool CanRemoveWallet(object parameter)
        {
            // Проверка, можно ли удалить кошелек
            return SelectedWallet != null;
        }

        private void AddTransaction(object parameter)
        {
            var newWallet = new Wallet
            {
                Id = Guid.NewGuid(),
                Name = "Новый кошелек", // Значение должно быть получено от пользователя
                Balance = 0,
                CurrencyId = Guid.NewGuid(),/* Укажите здесь ID существующей валюты */
    };

            try
            {
                _context.Wallets.Add(newWallet);
                _context.SaveChanges();
                Wallets.Add(newWallet); // Обновляем UI
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении кошелька: {ex.Message}");
            }
        }

        private void RemoveTransaction(object parameter)
        {
            // Предполагаем, что parameter это ID транзакции для удаления
            var transactionId = (Guid)parameter;
            var transactionToRemove = _context.Transactions.FirstOrDefault(t => t.Id == transactionId);

            if (transactionToRemove == null) return;

            try
            {
                _context.Transactions.Remove(transactionToRemove);
                _context.SaveChanges();
                // Здесь также стоит обновить коллекцию в UI, если это необходимо
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении транзакции: {ex.Message}");
            }
        }

        private bool CanRemoveTransaction(object parameter)
        {
            // Проверка, можно ли удалить транзакцию
            return true; 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
