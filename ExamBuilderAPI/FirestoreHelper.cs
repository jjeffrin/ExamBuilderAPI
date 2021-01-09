using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace ExamBuilderAPI
{
    public class FirestoreHelper<T> : IFirestoreHelper<T>
    {
        private readonly ILogger<FirestoreHelper<T>> _logger;
        private readonly string _path;
        private readonly string _projectId = "hodportal";
        private FirestoreDb _db;

        public FirestoreHelper(ILogger<FirestoreHelper<T>> logger)
        {
            _logger = logger;
            _path = ".\\hodportal-6e0a68d346c2.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", _path);
            _db = FirestoreDb.Create(_projectId);
        }

        public async Task<T> Delete(string collName, string id)
        {
            var docToDelete = await _db.Collection(collName).Document(id).GetSnapshotAsync();
            var deletedDoc = await _db.Collection(collName).Document(id).DeleteAsync();
            if (deletedDoc.UpdateTime > docToDelete.UpdateTime)
            {
                var data = default(T);
                return data;
            }
            else
            {                
                return ConvertToUsableData(docToDelete);
            }
        }

        public async Task<IEnumerable<T>> Get(string collName, string id)
        {
            var dataToReturn = new List<T>();
            if (id == null)
            {
                var docSnapshot = await _db.Collection(collName).GetSnapshotAsync();
                foreach (var doc in docSnapshot)
                {
                    if (doc.Exists)
                    {
                        dataToReturn.Add(ConvertToUsableData(doc));
                    }
                }
            }
            else
            {
                var docSnapshot = await _db.Collection(collName).Document(id).GetSnapshotAsync();
                if (docSnapshot.Exists)
                {
                    dataToReturn.Add(ConvertToUsableData(docSnapshot));
                }
            }
            
            return dataToReturn;
        }

        public async Task<T> Post(string collName, T objToSave)
        {
            T data = default(T);
            // Convert the obj to save to dictionary to send to firestore
            var dict = objToSave.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name.ToLower(), prop => (string)prop.GetValue(objToSave, null));
            // remove the empty Id key from the dict. Because firestore will assign it for us!
            dict.Remove("id");
            // Add the dict to firestore
            var addedDoc = await _db.Collection(collName).AddAsync(dict);
            // return back the added data with its allocated id
            var docSnapshot = await addedDoc.GetSnapshotAsync();
            if (docSnapshot.Exists)
            {
                data = ConvertToUsableData(docSnapshot);
            }
            return data;
        }

        public async Task<T> Put(string collName, T objToUpdate)
        {
            T data = default(T);
            // Convert the obj to save to dictionary to send to firestore
            var dict = objToUpdate.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name.ToLower(), prop => (string)prop.GetValue(objToUpdate, null));
            var id = dict["id"];
            // get the doc to update
            var docToUpdate = await _db.Collection(collName).Document(id).GetSnapshotAsync();
            // get the prev update time
            var prevUpdatedTime = docToUpdate.UpdateTime;
            // remove id from the dict. because firestore already gave us the id!
            dict.Remove("id");
            // do the update process with the new data!
            var updateResult = await _db.Collection(collName).Document(id).SetAsync(dict);
            // if the new update time is greater than prev, then update successful! return the new data, else return old data
            if (updateResult.UpdateTime > prevUpdatedTime)
            {
                return objToUpdate;
            }
            else
            {
                data = ConvertToUsableData(docToUpdate);
                return data;
            }
        }

        #region Helper Methods

        private static T ConvertToUsableData(DocumentSnapshot docToUpdate)
        {
            T data;
            var dict = docToUpdate.ToDictionary();
            dict.Add("Id", docToUpdate.Id);
            var json = JsonConvert.SerializeObject(dict);
            data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        #endregion
    }
}
