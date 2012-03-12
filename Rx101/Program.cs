using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace Rx101
    {
    class Program
        {
        static void Main(string[] args)
        {
            //Newspaper();
            //ObservableRange();
            //ObservableGenerate();
            //EnumerableToObservable();
            //CreateFromObserver();
            Console.ReadLine();
        }

            private static void Newspaper()
            {
                var start = Observable.Start(
                    () =>
                        {
                          //  for (var i = 0; i < 2; i++)
                         //   {
                                var newspaper = Observable.Interval(TimeSpan.FromMilliseconds(2500), Scheduler.NewThread);
                                var work = Observable.Interval(TimeSpan.FromMilliseconds(500), Scheduler.CurrentThread);
                                newspaper.Subscribe(value => Console.WriteLine("I got a newspaper"));
                                work.Subscribe(value => Console.WriteLine("I am working !!!"));
                                Thread.Sleep(1000);
                           // }
                        }
                    );
                start.Subscribe(x => Console.WriteLine("Action completed"));
            }
           
            ///<summary>
            /// This method creates an Observable stream of data starting from 100 for 20 data points.
            /// It takes in 3 Parametrized Action delegates as element Handler, Exception Handler and Completed Handler  
           /// </summary>
            private static void ObservableRange()
            {
            IObservable<int> source = Observable.Range(100,20);
            
            using (source.Subscribe(
                                    x => Console.WriteLine("OnNext: {0}", x),
                                    ex => Console.WriteLine("OnError: {0}", ex.Message),
                                    () => Console.WriteLine("OnCompleted")
                                    )
                   )
                {
                Console.WriteLine("I am done observing");
                }
            }
            
            ///<summary>
            /// This method creates an Observable stream of data using the iterator pattern , we specify the 
            /// selection initial condition , iterator , resultSelector and Iterator
            /// </summary>
            private static void ObservableGenerate()
            {

            IObservable<int> source = Observable.Generate(0, i => i < 5,
                                                                i => i + 1,
                                                                i => i * i,
                                                                i => TimeSpan.FromSeconds(i)
                                                          );
            using (source.Subscribe(
                                    x => Console.WriteLine("OnNext: {0}", x),
                                    ex => Console.WriteLine("OnError: {0}", ex.Message),
                                    () => Console.WriteLine("OnCompleted")
                                    )
                   )
                {
                Console.WriteLine("I am done generating");
                }
            }
            
            ///<summary>
            /// This method creates an Observable stream of data from an Pull based sequence. 
            /// This reflects the ability to transition from pull to push and vice-versa
            /// </summary>
            private static void EnumerableToObservable()
            {
                var source = Enumerable.Range(1,10).ToObservable();
                source.Subscribe(
                                    x => WriteOut(x)
                                );
            }
            
            ///<summary>
            /// This method creates an Observable stream of data from an Push based sequence
            /// </summary>
            private static void CreateFromObserver()
            {
                var source = Observer.Create(WriteOut,WriteOutException);
                source.OnNext(300);
                source.OnError(new IndexOutOfRangeException());
            }

            private static readonly Action<int> WriteOut = x => Console.WriteLine(DivideIt(x));
            private static readonly Action<Exception> WriteOutException = ex => Console.WriteLine(ex.Message);

            private static readonly Func<int, int> SquareIt = x => x*x;
            private static readonly Func<int, int> DivideIt = x => 100 / x;

        }
    }