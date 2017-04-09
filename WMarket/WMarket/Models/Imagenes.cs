﻿
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    [Serializable]
    public class Imagenes
    {
        public ObjectId _id { get; set; }
        public string FileName { get; set; }
        public string PictureDataAsString { get; set; }
        public int codPro { get; set; }
    }
}