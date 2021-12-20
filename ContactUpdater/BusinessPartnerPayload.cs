using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ContactUpdater
{
    public class BusinessPartnerPayload
    {
        public List<ContactEmployees> ContactEmployees = new List<ContactEmployees>();
    }
    
    public class ContactEmployees
    {
        //public string E_Mail { get; set; }
        public int InternalCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}