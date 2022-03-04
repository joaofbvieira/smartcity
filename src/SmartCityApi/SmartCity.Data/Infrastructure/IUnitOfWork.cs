using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
