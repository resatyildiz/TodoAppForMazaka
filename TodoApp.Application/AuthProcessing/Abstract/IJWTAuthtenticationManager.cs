using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Application.AuthProcessing
{
    public interface IJWTAuthtenticationManager
    {
        string Authenticate(string username, string password);
    }

}
