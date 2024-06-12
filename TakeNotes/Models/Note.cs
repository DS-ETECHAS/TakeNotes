using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TakeNotes.Models
{
    public class Note
    {
        private int _id;
        private string _title;
        private string _userId;
        private bool _completed;

        public int Id { get => _id; set => _id = value; }
        public string Title { get => _title; set => _title = value; }
        public string UserId { get => _userId; set => _userId = value; }
        public bool Completed { get => _completed; set => _completed = value; }
    }
}
