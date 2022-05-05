using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpProxy
{
    public class Movement
    {
        public DateTime timeStamp { get; set; }
        public string movement { get; set; }


        public Movement()
        {
        }

        public Movement(string movement, DateTime timestamp)
        {
            this.movement = movement;
            timeStamp = timestamp;
        }
    }
}
