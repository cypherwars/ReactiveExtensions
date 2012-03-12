using System;
using System.Reactive.Subjects;

namespace Rx102Subjects
    {
    class Program
        {
            static void Main(string[] args)
                {
                    Console.WriteLine("Subject");
                    Subject();
                    Console.WriteLine("ReplaySubject");
                    ReplaySubject();
                    Console.WriteLine("BehaviourSubject");
                    BehaviourSubject();
                    Console.WriteLine("AsyncSubject");
                    AsyncSubject();
                    Console.ReadLine();
                }
            
            ///<summary>
            ///Subject<T> will listen to all publications once subscribed. 
            ///The subscriber will miss all publications made before subscription. 
            /// </summary>
            private static void Subject()
                {
                var subject = new Subject<string>();
                subject.Subscribe( x => Console.WriteLine("Subscritipon 1 : " + x));
                subject.OnNext("a");
                subject.Subscribe(x => Console.WriteLine("Subscritipon 2 : " + x));
                subject.OnNext("b");
                subject.OnNext("c");
                }
           
            ///<summary>
            ///ReplaySubject<T> will listen to all publications once subscribed. 
            ///The subscriber will also get all publications made before subscription.
            ///Simply, ReplaySubject has a buffer in whihc it will keep all the publications made for future subscriptions.
            ///</summary>
            private static void ReplaySubject()
                {
                var subject = new ReplaySubject<string>();
                subject.OnNext("a");
                subject.Subscribe(Console.WriteLine);
                subject.OnNext("b");
                subject.OnNext("c");
                }
           
            ///<summary>
            ///With BehaviourSubject<T> ,the subscriber will only get all the last publication made
            ///Simply, BehaviourSubject has a one value buffer. Hence, it requires a default value.
            ///</summary>
            private static void BehaviourSubject()
                {
                var subject = new BehaviorSubject<string>("Rx");
                subject.OnNext("a");
                var d = subject.Subscribe(x => Console.WriteLine("Subscritipon 1 : " + x));
                subject.OnNext("b");
               // var d = subject.Subscribe(x => Console.WriteLine("Subscritipon 1 : " + x));
                d.Dispose(); 
                subject.OnNext("c");
                subject.Subscribe(x => Console.WriteLine("Subscritipon 2 : " + x));
                }
           
            ///<summary>
            ///With AsyncSubject<T> ,the subscriber will only get all the last publication made.
            ///Simply, AsyncSubject has a one value buffer. The publication is made only on the OnCompleted() call.
            ///</summary>
            private static void AsyncSubject()
                {
                var subject = new AsyncSubject<string>();
                subject.OnNext("a");
                subject.OnNext("b");
                subject.OnNext("c");
                subject.Subscribe(Console.WriteLine);
                subject.OnCompleted();
                }
        }
    }