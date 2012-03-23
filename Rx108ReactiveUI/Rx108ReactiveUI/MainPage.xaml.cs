using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;

namespace ReactiveUI
    {
    public partial class MainPage
    {
        public MainPage()
            {
            InitializeComponent();
            }

        public static IEnumerable<string> GetModel()
            {
            for (var i = 0; i < 5; ++i)
                {
                System.Threading.Thread.Sleep(1000);
                yield return "Item " + i;
                }
            }

        private void NonRxButtonClick(object sender, RoutedEventArgs e)
            {
            var items = new ObservableCollection<string>();
            MyComboBox.ItemsSource = items;
            foreach (var item in GetModel())
                items.Add(item);
            }

      private void RxButtonClick(object sender, RoutedEventArgs e)
            {
            var items = new ObservableCollection<string>();
            MyComboBox.ItemsSource = items;
            IObservable<string> observable = GetModel().ToObservable();
            observable.Subscribe(items.Add);
            }

      private void RxWithSchedulerClick(object sender, RoutedEventArgs e)
          {
            var items = new ObservableCollection<string>();
            MyComboBox.ItemsSource = items;
            IObservable<string> observable = GetModel().ToObservable(Scheduler.NewThread);
            observable.ObserveOnDispatcher().Subscribe(item => items.Add(item));
          }
        }
    }