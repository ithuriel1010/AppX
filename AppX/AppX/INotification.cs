using System;
using System.Collections.Generic;
using System.Text;

namespace AppX
{
    public interface INotification
    {
        void CreateNotification(string title, string message, string action);
    }
}
