using Contacts.UseCases.PluginInterfaces;
using SQLite;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.SQLLite
{
    public class ContactSQLiteRepository : IContactRepository
    {
        private SQLiteAsyncConnection database;

        public ContactSQLiteRepository()
        {
            database = new SQLiteAsyncConnection(Constants.DatabasePath);
            database.CreateTableAsync<Contact>();   // Create table if it does not exist.
        }

        public async Task AddContactAsync(Contact contact)
        {
            await database.InsertAsync(contact);
        }

        public async Task DeleteContactAsync(int contactId)
        {
            var contact = await GetContactByIdAsync(contactId);
            if (contact != null && contact.ContactId == contactId)
                await database.DeleteAsync(contact);
        }

        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
            return await database.Table<Contact>().Where(x => x.ContactId == contactId).FirstOrDefaultAsync();
        }

        public async Task<List<Contact>> GetContactsAsync(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
                return await database.Table<Contact>().ToListAsync();

            return await database.QueryAsync<Contact>(@"
                        SELECT * 
                        FROM Contact
                        WHERE 
                            Name LIKE ? OR 
                            Email LIKE ? OR
                            Phone LIKE ? OR
                            ImagePath LIKE ?",
                            $"{filterText}%",
                            $"{filterText}%",
                            $"{filterText}%",
                            $"{filterText}%");
        }

        public async Task UpdateContact(int contactId, Contact contact)
        {
            if (contactId == contact.ContactId)
                await database.UpdateAsync(contact);
        }
    }
}