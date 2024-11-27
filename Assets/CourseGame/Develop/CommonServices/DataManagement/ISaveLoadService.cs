using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CourseGame.Develop.CommonServices.DataManagement
{
    public interface ISaveLoadService
    {
        bool TryLoad<TData>(out TData data) where TData : ISaveData;
        void Save<TData>(TData data) where TData : ISaveData;
    }
}
