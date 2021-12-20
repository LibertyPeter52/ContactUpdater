using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B1SLayer;

namespace ContactUpdater
{
    internal static class Program
    {
        private static async Task Main()
        {
            var serviceLayer = new SLConnection("https://redacted:50000/b1s/v2", "redacted", "manager", "redacted");

            var businessPartners = await serviceLayer
                .Request($"BusinessPartners")
                .Filter($@"CardType eq 'C'")
                .WithPageSize(333)
                .GetAsync<List<BusinessPartner>>();

            foreach (var businessPartner in businessPartners.Where(businessPartner => businessPartner.ContactEmployees.Count > 0))
            {
                foreach (var contactEmployee in businessPartner.ContactEmployees)
                {
                    var names = contactEmployee.Name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (names.Length > 0)
                    {
                        contactEmployee.FirstName = names[0];
                    }

                    switch (names.Length)
                    {
                        case 3:
                            contactEmployee.MiddleName = names[1];
                            contactEmployee.LastName = names[2];
                            break;
                        case 2:
                            contactEmployee.LastName = names[1];
                            break;
                    }

                    if (contactEmployee.E_Mail != null)
                        contactEmployee.E_Mail = contactEmployee.E_Mail.ToUpper();
                }
                try
                {
                    await serviceLayer
                        .Request($"BusinessPartners('{businessPartner.CardCode}')")
                        .PatchAsync(businessPartner);
                    Console.WriteLine($"Success updating {businessPartner.CardCode}");
                }
                catch
                {
                    Console.WriteLine($"Failed updating {businessPartner.CardCode}");
                }
            }
            await serviceLayer.LogoutAsync();
        }
    }
}
