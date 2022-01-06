using System;
using System.IO;
using System.Net;
using RestSharp;
using RestSharp.Portable;
using System.Threading.Tasks;
using Method = RestSharp.Method;
using RestRequest = RestSharp.RestRequest;
using RestResponse = RestSharp.RestResponse;

namespace ClientApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1: GetAll, 2: Delete, 3: Downolad, 4: Post");

            int input = int.Parse(Console.ReadLine());

            if (input==1)
            {
                //GetAll
                var url = "https://localhost:5001/api/Files/GetAll";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Accept = "application/json";


                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                
                    Console.WriteLine(result);
                }

                Console.WriteLine(httpResponse.StatusCode);
            }
            else if(input==2)
            {
                //Delete
                Console.WriteLine("Id: ");
                int id = int.Parse(Console.ReadLine());
                var url = "https://localhost:5001/api/Files/Delete/"+$"{id}";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "DELETE";

                httpRequest.Accept = "application/json";


                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine(result);
                }

                Console.WriteLine(httpResponse.StatusCode);
            }
            else if(input==3)
            {
                //Download
                Console.WriteLine("Id: ");
                int id = int.Parse(Console.ReadLine());
                var url = "https://localhost:5001/api/Files/DownloadFile/"+$"{id}";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Accept = "application/json";


                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                }
                Console.WriteLine(httpResponse.StatusCode +" file download");
            }

            else if (input==4)
            {
                //Post
                var client = new RestClient("https://localhost:5001/api/Files/PostFile");
                var request = new RestRequest(Method.Post.ToString());
                var path=request.AddFile("FilePath", "C:/Users/Admin/Documents/rustProject/ClientApi/1.jpg");
                Task<RestResponse> response = client.ExecuteAsync(path);
                Console.WriteLine(response.Result);
                
                Console.WriteLine("Post file");
            }

        }
    }
}
