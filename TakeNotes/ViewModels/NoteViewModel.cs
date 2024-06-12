using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TakeNotes.Models;
using TakeNotes.Services;

namespace TakeNotes.ViewModels
{
    public partial class NoteViewModel : ObservableObject
    {
        [ObservableProperty]
        public string userId;
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string _title;
        [ObservableProperty]
        public bool completed;
        
        [ObservableProperty]
        public ObservableCollection<Note> notes;

        public ICommand SaveItemCommand { get; }
        public ICommand GetItensCommand { get; }
        public ICommand DeleteItemCommand { get; }
        private NoteService _noteService;

        public NoteViewModel() {
            notes = new ObservableCollection<Note>();
            _noteService = new NoteService();
            GetItensCommand = new Command(GetItemsAsync);
            SaveItemCommand = new Command(SaveItem);
            DeleteItemCommand = new Command(DeleteItem);
            userId = Guid.NewGuid().ToString();
            GetItensCommand.Execute(this);
        }

        public async void SaveItem()
        {
            Note note = new Note();
            note.UserId = userId;
            note.Id = id;
            note.Title = _title;
            note.Completed = completed;
            //await _noteService.SaveNoteAsync(note);
            Notes.Add(note);
            _noteService.SaveNotesToFileAsync(Notes);
        }
        public void DeleteItem()
        {
            Note note = new Note();
            note.UserId = userId;
            note.Id = id;
            note.Title = _title;
            note.Completed = completed;
            Notes.Remove(note);
            _noteService.SaveNotesToFileAsync(Notes);
        }

        public async void GetItemsAsync()
        {
            //Notes = await _noteService.GetNotesAsync();
            Notes = await _noteService.GetNotesFromFileAsync();
        }

    }
}
