﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using EventMaker.Model;

namespace EventMaker.Persistancy
{
    public class PersistancyService
    {
        //-------------------------------------Serialization----------------------------------
        //Auto propery
        public ObservableCollection<Event> EventsCatalog { get; set; }

        //saving into the file
        public async Task SavetoJson(ObservableCollection<Event> events)
        {
            EventsCatalog = events;
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.CreateFileAsync("EventsFile0.txt", CreationCollisionOption.ReplaceExisting);
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Event>));
            using (var stream = await jsonFile.OpenStreamForWriteAsync())
            {
                jsonSerializer.WriteObject(stream, EventsCatalog);
            }
        }

        //retriving from the file
        public async Task<ObservableCollection<Event>> LoadFromJson()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.GetFileAsync("EventsFile0.txt");
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Event>));
            using (var stream = await jsonFile.OpenStreamForReadAsync())
            {
                EventsCatalog = jsonSerializer.ReadObject(stream) as ObservableCollection<Event>;
            }
            return EventsCatalog;
        }
    }
}
