using Contact.API.DTO;

namespace Contact.API.Mapper
{
    public static class ModelConverter
    {
        public static Contact.API.Entities.Contact DtoToModel(ContactDto model)
        {
            return new Contact.API.Entities.Contact
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Favorite = model.Favorite
            };
        }
        /// <summary>
        /// We have two way two map our Model with DTO. Model to DTO & DTO to Model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ContactDto ModelToDto(Contact.API.Entities.Contact model)
        {
            return new ContactDto
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Favorite = model.Favorite
            };
        }
    }
}
