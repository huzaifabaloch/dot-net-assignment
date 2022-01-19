using System;
using System.Collections.Generic;
using System.Text;

namespace Problem2
{
    /// <summary>
    /// This class model has 2 members, 
    /// 1. path -> this will hold the absolute path of a folder.
    /// 2. children -> this may contain more of same type objects.
    /// I used a constrcutor to initialize the class model.
    /// </summary>
    public class Branch
    {
        public string path;
        public List<Branch> children;

        public Branch(string path)
        {
            this.path = path;
            children = null;
        }
    }
}
