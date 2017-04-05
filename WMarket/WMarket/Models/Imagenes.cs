using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks;

namespace WMarket.Models
{
    public class Imagenes
    {
        static void Main(string[] args)
        {
            var client = new MongoClient();  //conecta cliente
            var database = client.GetDatabase("ImageAmazon"); //busca DB

            var fs = new GridFSBucket(database);

            var id = UploadFile(fs); //Funcion para subir

            //DownloadFile(fs, id);  //funcion para bajar
        }

        private static ObjectId UploadFile(GridFSBucket fs)
        {
            using (var s = File.OpenRead(@"C:\Users\Erick\Downloads\LaptopHP.png"))
            {
                var t = Task.Run<ObjectId>( () => {
                    return fs.UploadFromStreamAsync("LaptopHP.png", s);
                });

                return t.Result;
            }
        }

        private static void DownloadFile(GridFSBucket fs, ObjectId id)
        {
            var t = fs.DownloadAsBytesByNameAsync("LaptopHP.png");
            Task.WaitAll(t);
            var bytes = t.Result;
        }
    }
}