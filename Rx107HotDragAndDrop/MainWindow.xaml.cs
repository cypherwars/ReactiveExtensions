using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Rx107HotDragAndDrop
    {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
        public MainWindow()
            {
            InitializeComponent();

            var mousedown = from e in Observable.FromEventPattern <MouseButtonEventArgs>(this,"MouseDown") select e.EventArgs.GetPosition(this);
            var mouseup = from e in Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseUp") select e.EventArgs.GetPosition(this);
            var mousemove = from e in Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseMove") select e.EventArgs.GetPosition(this);

            var xs = from start in mousedown
                        from pos in mousemove.StartWith(start).TakeUntil(mouseup)
                        select pos;
            var deltas = from start in mousedown
                         from pair in mousemove.StartWith(start).TakeUntil(mouseup).Buffer(2)
                         let array = pair.ToArray()
                         let a = array[0]
                         let b = array[1]
                         select new Size(b.X - a.X, b.Y - a.Y).ToString();

    
            }
        }
    }
