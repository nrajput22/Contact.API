using Contact.API.DTO;

namespace Contact.API.Repository
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactDto>> GetAllContacts();
        Task<ContactDto> GetContactById(int id);
        Task<ContactDto> CreateUpdateContactAsync(ContactDto contactDto);
        Task<bool> DeleteContactAsync(int id); 
    }
}
