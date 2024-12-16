using Contact.API.Data;
using Contact.API.DTO;
using Contact.API.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Contact.API.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext contactDbContext;

        public ContactRepository(ContactDbContext contactDbContext)
        {
            this.contactDbContext = contactDbContext;
        }

        public async Task<ContactDto> CreateUpdateContactAsync(ContactDto contactDto)
        {
            var contact = ModelConverter.DtoToModel(contactDto);
            if (contact.Id > 0)
            {
                contactDbContext.Contacts.Update(contact);
            }
            else
            {
                await contactDbContext.Contacts.AddAsync(contact);
            }
            await contactDbContext.SaveChangesAsync();
            var dtoContact = ModelConverter.ModelToDto(contact);
            return dtoContact;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await contactDbContext.Contacts.FirstOrDefaultAsync(p => p.Id == id);
            if (contact == null)
            {
                return false;
            }
            contactDbContext.Contacts.Remove(contact);
            await contactDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ContactDto>> GetAllContacts()
        {
            var contacts = await contactDbContext.Contacts.Select(contacts =>
            ModelConverter.ModelToDto(contacts)).ToListAsync();
            return contacts;
        }

        public async Task<ContactDto> GetContactById(int id)
        {
            var contact = await contactDbContext.Contacts.Select(contact =>
            ModelConverter.ModelToDto(contact)).FirstOrDefaultAsync();
            return contact;
        }
    }
}
