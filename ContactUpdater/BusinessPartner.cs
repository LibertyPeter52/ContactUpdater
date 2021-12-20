using System.Collections.Generic;
using Newtonsoft.Json;

namespace ContactUpdater
{
    public class BusinessPartner
    {
        public string CardCode { get; set; }
        public List<ContactEmployee> ContactEmployees = new List<ContactEmployee>();
    }
    public class ContactEmployee
    {
        public string Name { get; set; }
        public string E_Mail { get; set; }
        public int InternalCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}