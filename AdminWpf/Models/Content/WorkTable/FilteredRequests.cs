using AuthServer.Models.Content.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.Models.Content.WorkTable
{
    class FilteredRequests
    {
        public IEnumerable<FWRequest> GetFilteredData(
            IEnumerable<FWRequest> data, 
            DateTime from, 
            DateTime to)
        {            
            data = data.Where(u => u.CreatedTime >= from).ToList();
            data = data.Where(u => u.CreatedTime <= to).ToList();
            return data;
        }
    }
}
