using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace Rx103SomeOperators
    {
    class Program
        {
            static Random rnd = new Random();

            static void Main(string[] args)
            {
                var source = NonBlocking();
                source.Subscribe(Console.WriteLine);
                //Interval();
                //RollingSum();
                //Merge();
                //Concat();
               // SimpleConnectSample();
                //ComplexConnectSampleWithSideEffects();
                Console.ReadLine();
            }

            ///<summary>
        ///The Method simulates a blocking call by assigning the Immediate Thread as the Thread of execution
        /// The execution will move into asyncmode when we call the Nonblocking Method
        /// </summary>
            private static IObservable<string> BlockingMethod()
            {
              var subject = new ReplaySubject<string>(Scheduler.Immediate);
              subject.Subscribe(Console.WriteLine);
              subject.OnNext("a");
              subject.OnNext("b");
              subject.OnCompleted();
              Thread.Sleep(2000);
              return subject;
            }
           
            ///<summary>
            ///The Method simulates a Non-blocking call by using Observable.Create<T>(Func<IObserver<T>,IDisposable>)
            /// </summary>
            private static IObservable<string> NonBlocking()
            {
            //Observable.Create<T>(Func<IObserver<T>,IDisposable>)
            //Observable.Create<T>(Func<IObserver<T>,Action>)
                return Observable.Create<string>(
                    observable =>
                        {
                            observable.OnNext("c");
                            observable.OnNext("d");
                            //Uncomment the below code when running the NonBlocking Method from Main

                            Thread.Sleep(1000);
                            var source = BlockingMethod();
                            source.Subscribe(Console.WriteLine);
                            Thread.Sleep(1000);
                            observable.OnNext("e");
                            observable.OnNext("f");
                            
                            observable.OnCompleted();

                            return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed"));
                            //or can return an Action like
                            //return () => Console.WriteLine("Observer has unsubscribed");
                        });
            }

            ///<summary>
            ///The Method simulates a regular interval publishing through the Interval method.
            ///Say the price goes above x we are taking some Action and doing something else otherwise.
            ///Such situation is common in stock tickers. Publishing error messagesover the network , waiting for someone to subscribe.
            /// </summary>
            private static void Interval()
                {
                
                var lastPrice = 100.0;
                var interval = Observable.Interval(TimeSpan.FromMilliseconds(250))
                    .Select(i =>
                    {
                        var variation = rnd.NextDouble() - 0.5;
                        lastPrice += variation;
                        return lastPrice;
                    });

               // var disposable = interval.Subscribe(Console.WriteLine);

                interval.ForEach(
                        d => Console.WriteLine(d > 99.5 ? "Hurray Price above 99.5 " : "Yikesssss"));
                }
            
            ///<summary>
            ///The Method calculates a rolling sum by using the Scan extension Method of the IObservable<T> interface
            /// </summary>
            private static void RollingSum()
            {
                var interval = Observable.Interval(TimeSpan.FromMilliseconds(150)).Take(10);
                var scan = interval.Scan(0L, (seed, i) => seed + i);
                scan.Subscribe(Console.WriteLine);
            }

            ///<summary>
            ///The Merge Operator merges two streams as if they were cming from a single data source
            /// </summary>
            private static void Merge()
            {
            //Generate values 0,1,2
            var stream1 = Observable.Interval(TimeSpan.FromMilliseconds(250)).Take(3);
            //Generate values 100,101,102,103,104
            var stream2 = Observable.Interval(TimeSpan.FromMilliseconds(150)).Take(5).Select(i => i + 100);
            stream1
                .Merge(stream2)
                .Subscribe(Console.WriteLine);
            }

            ///<summary>
            ///The Concat Operator concatenates twostreams. 
            ///If the first stream errors out thne the second stream will never be used.
            /// </summary>
            private static void Concat()
            {
            //Generate values 0,1,2
            var stream1 = Observable.Generate(0, i => i < 3, i => i+1, i => i );
            //Generate values 100,101,102,103,104
            var stream2 = Observable.Generate(100, i => i < 105, i => i+1, i => i);

            stream1
                .Concat(stream2)
                .Subscribe(Console.WriteLine);
            }

            ///<summary>
            ///The Publish operator returns an IConnectableObservable<T> which can be used to connect 
            /// multiple subscriptions to the single data streams 
            /// </summary>
            private static void SimpleConnectSample()
                {
                var period = TimeSpan.FromSeconds(1);
                //Publish makes a Cold Observable Hot
                var observable = Observable.Interval(period).Take(5).Publish();
                observable.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
                //Thread.Sleep(period);
                observable.Subscribe(i => Console.WriteLine("second subscription : {0}", i));
                observable.Connect();
                }
            
            ///<summary>
            ///The method shows how publishing happens in Hot Observables , i.e. a side effect is there 
            /// subscribers may or may not be subscribed at the time of publishing and hence will
            /// only see the data when they hop onto the stream
            /// </summary>
            private static void ComplexConnectSampleWithSideEffects()
                {
                var period = TimeSpan.FromSeconds(1);
                var observable = Observable.Interval(period)
                  .Do(l => Console.WriteLine("Publishing {0}", l)) //produce Side effect to show it is running.
                  .Publish();
                observable.Connect();
                Console.WriteLine("Press any key to subscribe");
                Console.ReadKey();
                var subscription = observable.Subscribe(i => Console.WriteLine("subscription : {0}", i));

                Console.WriteLine("Press any key to unsubscribe.");
                Console.ReadKey();
                subscription.Dispose();
                }

        }
    }