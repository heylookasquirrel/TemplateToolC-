using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTool
{
    class Ticket
    {
        private string _comments;
        private string _trail;
        private string[] _steps;
        private string _location;
        private string _schedule;
        private string _manager;
        private string _rto;
        private DateTime _timestamp;

        public Ticket(string comments, string trail, string[] steps, string location, string schedule, string manager, string rto)
        {
            this._comments = comments;
            this._trail = trail;
            this._steps = steps;
            this._location = location;
            this._schedule = schedule;
            this._manager = manager;
            this._rto = rto;

        }

        public string[] steps
        {
            get
            {
                return _steps;
            }
            set
            {
                this._steps = value;
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                this._comments = value;

            }
        }

        public string Trail
        {
            get
            {
                return _trail;
            }
            set
            {
                this._trail = value;
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                this._location = value;
            }
        }

        public string Schedule
        {
            get
            {
                return _schedule;
            }
            set
            {
                this._schedule = value;
            }
        }
        public string Manager
        {
            get
            {
                return _manager;
            }
            set
            {
                this._manager = value;
            }
        }

        public string Rto
        {
            get
            {
                return _rto;
            }
            set
            {
                this._rto = value;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                this._timestamp = value;
            }
        }

    }
}
