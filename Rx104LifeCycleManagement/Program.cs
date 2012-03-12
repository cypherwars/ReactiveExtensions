using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace Rx104LifeCycleManagement
    {
    class Program
        {
        static void Main(string[] args)
            {
              LifeCycleManagement();
            //Thread.CurrentThread.Name = "Main Thread";
            //var stream = Observable.Interval(TimeSpan.FromMilliseconds(100)).Take(10);
            //var failover = Observable.Interval(TimeSpan.FromMilliseconds(500)).Take(5);
            //FailoverSample(stream, failover);
            Console.ReadLine();
            }

        private static void LifeCycleManagement()
            {
            var interval = Observable.Interval(TimeSpan.FromMilliseconds(100),Scheduler.NewThread)
                                                .Do(l => Console.WriteLine("Publishing {0}", l));
           
            var firstSubscription =
                interval.Subscribe(value => Console.WriteLine("1st subscription recieved {0}", value));

            Thread.Sleep(500);

            var secondSubscription =
                interval.Subscribe(value => Console.WriteLine("2nd subscription recieved {0}", value));

            Thread.Sleep(500);

            firstSubscription.Dispose();
            Console.WriteLine("Disposed of 1st subscription");
            Thread.Sleep(500);
            secondSubscription.Dispose();
            Console.WriteLine("Disposed of 2nd subscription");
            }

        public static void FailoverSample<T>(IObservable<T> stream, IObservable<T> failover)
            {
            stream
                .OnErrorResumeNext(failover)
                .Subscribe(t => Console.WriteLine(t), ex => Console.WriteLine(ex.Message));
            Console.WriteLine("Hello" +Thread.CurrentThread.Name);
            }
        }
    }