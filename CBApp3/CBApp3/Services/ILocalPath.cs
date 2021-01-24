using System;
using System.Collections.Generic;
using System.Text;

namespace CBApp3.Services
{
    public interface ILocalPath
    {
        string GetFullPath(string fileName);
    }
}
