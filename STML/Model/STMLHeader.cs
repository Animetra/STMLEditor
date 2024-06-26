﻿using System;

namespace STML.Model
{
    public sealed class STMLHeader
    {
        public string Name { get; set; }
        public string ID { get; }
        public string Comments { get; set; }

        public STMLHeader(string name = "New STML Element", string id = "", string comments = "")
        { 
            Name = name;
            ID = id is null || id == "" ? Guid.NewGuid().ToString() : id;
            Comments = comments;
        }
    }
}
