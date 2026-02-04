using System;
using System.Collections.Generic;
using System.Text;

namespace Usta.Infrastructure.Cache.Contracts
{
    public interface ICacheService
    {
        void SetSliding<T>(string key, T data, int expiretionTime);

        void Set<T>(string key, T data, int expiretionTime);

        T Get<T>(string key);

        void Remove(string key);
    }
}