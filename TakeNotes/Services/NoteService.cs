using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TakeNotes.Models;


namespace TakeNotes.Services
{
    class NoteService
    {
        private string filePath;
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        public ObservableCollection<Note> Items { get; private set; }
  

        public NoteService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            Items = new ObservableCollection<Note>();
            filePath = String.Concat(FileSystem.AppDataDirectory, "NotesApp.config");
            
        }
       
        public async Task<ObservableCollection<Note>> GetNotesAsync()
        {
            Uri uri = new Uri("https://jsonplaceholder.typicode.com/todos");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                   // Items = JsonSerializer.Deserialize<ObservableCollection<Note>>(content, _serializerOptions);
                   Items = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Note>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Items;
        }

        public async Task SaveNoteAsync(Note item)
        {
            Uri uri = new Uri("https://jsonplaceholder.typicode.com/todos");
            try
            {
                string json = JsonSerializer.Serialize<Note>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode) { 
                    Debug.WriteLine(@"\tItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task SaveNotesToFileAsync(ObservableCollection<Note> items)
        {
            try
            {
                
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(items);
                File.WriteAllText(filePath, json.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<ObservableCollection<Note>> GetNotesFromFileAsync()
        {
            try
            {
                    string fileContent = File.ReadAllText(filePath);
                    // Items = JsonSerializer.Deserialize<ObservableCollection<Note>>(content, _serializerOptions);
                    Items = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Note>>(fileContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Items;
        }

        public async Task UpdateNoteAsync(Note item)
        {
            Uri uri = new Uri("https://jsonplaceholder.typicode.com/todos");
            try
            {
                string json = JsonSerializer.Serialize<Note>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode) { 
                    Debug.WriteLine(@"\tItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
