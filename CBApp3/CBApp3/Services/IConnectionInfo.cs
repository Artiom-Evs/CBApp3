using System;
using System.Collections.Generic;
using System.Text;

namespace CBApp3.Services
{
    public interface IConnectionInfo
    {
        bool IsConnected { get; }
    }
}
