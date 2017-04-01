using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba7
{
    [Serializable]
    public class Account
    {   
        public int Number { set; get; }
        public string TypeofAmount { set; get; }
        public int Balance { set; get; }
        public string DataOfOpen { set; get; }
        public bool Sms { set; get; }
        public bool InternetBanking { set; get; }
        public Owner OwnerTemp { get; set; }
        public Operation OperationTemp { get; set; }


    }
}
