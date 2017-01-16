using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModSecurityLogService.Models
{
    public class SecAuditLog
    {
        public Transaction transaction { get; set; }
        public Request request { get; set; }
        public Response response { get; set; }
        public Audit_Data audit_data { get; set; }
    }

    public class Transaction
    {
        public string time { get; set; }
        public string transaction_id { get; set; }
        public string remote_address { get; set; }
        public int remote_port { get; set; }
        public string local_address { get; set; }
        public int local_port { get; set; }
    }

    public class Request
    {
        public string request_line { get; set; }
        public Headers headers { get; set; }
    }

    public class Headers
    {
        public string CacheControl { get; set; }
        public string Connection { get; set; }
        public string Accept { get; set; }
        public string AcceptEncoding { get; set; }
        public string AcceptLanguage { get; set; }
        public string Cookie { get; set; }
        public string Host { get; set; }
        public string UserAgent { get; set; }
        public string UpgradeInsecureRequests { get; set; }
        public string DNT { get; set; }
    }

    public class Response
    {
        public string protocol { get; set; }
        public int status { get; set; }
    }


    public class Audit_Data
    {
        public string[] messages { get; set; }
        public Action action { get; set; }
        public string handler { get; set; }
        public string[] producer { get; set; }
        public string server { get; set; }
        public string engine_mode { get; set; }
    }

    public class Action
    {
        public bool intercepted { get; set; }
        public int phase { get; set; }
        public string message { get; set; }
    }


}
