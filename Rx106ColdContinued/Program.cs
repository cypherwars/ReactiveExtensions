using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Rx106ColdContinued
    {
    class Program
        {

        private static readonly Func<List<String>, string, bool> FindByTag = ((x, y) => x.Contains(y));


        static void Main(string[] args)
            {
            var start = Observable.Start(() =>
            {
                Console.WriteLine("Getting some work done");

                for (var i = 0; i < 2; i++)
                    {
                    Thread.Sleep(10000);
                    //go call stackoverflow
                    var d = CallStackOverFlow();
                    //send the result for checking to a delegate
                    foreach (var q in d.questions)
                        {
                        var list = (q.tags as JArray).Values().Select(v => v.ToString()).ToList();
                        var result = FindByTag(list, "c#");
                        if (result)
                            Console.WriteLine("Quesion title :{0}", q.title);
                        }
                    }
                return "I am done observing";
            });
            Console.WriteLine("Subscribing"); // Make visible when we subscribe
            start.Subscribe(Console.WriteLine);
            Console.ReadKey();
            }
        
        private static dynamic CallStackOverFlow()
            {
            var client = new RestClient("http://api.stackoverflow.com/1.1");
            var request = new RestRequest("/questions/no-answers", Method.POST);
            var response = client.Execute(request);
            var content = response.Content; // raw content as string
            dynamic deserialised = JsonConvert.DeserializeObject(content);
            return deserialised;
            }
        }
    }
