using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TemplateTool
{
    public class DefaultText
    {
        string _comments;
        string _trail;
        string _location;
        string _schedule;
        string _manager;
        string _rto;
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

    }

}

