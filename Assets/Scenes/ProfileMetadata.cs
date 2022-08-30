using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileMetadata
{
    public Lsp3profile LSP3Profile ;
    [Serializable]
    public class Lsp3profile
    {
        public string name ;
        public string description ;
        public Link[] links ;
        public string[] tags ;
        public Profileimage[] profileImage ;
        public Backgroundimage[] backgroundImage ;
    }
    [Serializable]
    public class Link
    {
        public string title ;
        public string url ;
    }
    [Serializable]
    public class Profileimage
    {
        public int width ;
        public int height ;
        public string hashFunction ;
        public string hash ;
        public string url ;
    }
    [Serializable]
    public class Backgroundimage
    {
        public int width ;
        public int height ;
        public string hashFunction ;
        public string hash ;
        public string url ;
    }
}
