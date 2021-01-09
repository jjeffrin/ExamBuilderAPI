using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamBuilderAPI
{
    public interface IFirestoreHelper<T>
    {
        Task<IEnumerable<T>> Get(string collName, string id);
        Task<T> Delete(string collName, string id);
        Task<T> Post(string collName, T objToSave);
        Task<T> Put(string collName, T objToUpdate);
    }
}