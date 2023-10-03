using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases
{
    public class EditContactUseCase : IEditContactUseCase
    {
        private readonly IContactRepository _contactRepository;

        public EditContactUseCase(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task ExecuteAsync(int contactId, Contact contact)
        {
            await _contactRepository.UpdateContact(contactId, contact);
        }
    }
}
