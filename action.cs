using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
    class action
    {
        public string user { get; set; }
        public string action_type { get; set; }
        public string date { get; set; }
        public string action_result { get; set; }

        public action(string user, string action_type, string date, string action_result)
        {
            this.user = user;
            this.action_type = action_type;
            this.date = date;
            this.action_result = action_result;
        }

        public override string ToString()
        {
            return $"User or ID: {this.user} Action Type: {this.action_type}, Time & Date: {this.date}, Result: {this.action_result}";
        }
    }
}
