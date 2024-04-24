using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.Models.Content
{
    internal  class RequestStatuses
    {
        private  List<string> _statuses;

        public List<string> Statuses 
        { 
            get { return _statuses; } 
            private set { _statuses = value; }
        }

        public RequestStatuses()
        {
            _statuses =
            [
                "Получена",
                "В работе",
                "Выполнена",
                "Отклонена",
                "Отменена"
            ];
        }
    }
}
