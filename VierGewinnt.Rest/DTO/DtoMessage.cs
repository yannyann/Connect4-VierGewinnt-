using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Rest.DTO
{
    public class DtoMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }


        public DtoMessage()
        {

        }
        public DtoMessage(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }
    }
}
