using Contact.API.DTO;
using Contact.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _iContactRepository;

        public ContactController(IContactRepository iContactRepository)
        {
            _iContactRepository = iContactRepository;
        }


        // GET: api/<ContactController>
        [HttpGet]
        public async Task<object> Get()
        {
            var response = new ResponseDto();

            try
            {
                // Fetch the data
                IEnumerable<ContactDto> productDto = await _iContactRepository.GetAllContacts();

                // Set the response properties
                response.IsSuccess = true;
                response.Result = productDto;
                response.Message = "Contacts retrieved successfully";
            }
            catch (Exception ex)
            {
                // Handle exceptions and populate the response with error details
                response.IsSuccess = false;
                response.Errors = new List<string> { ex.Message };
                response.Message = "An error occurred while retrieving contacts.";
            }

            // Return the response wrapped in an IActionResult
            return Ok(response);
            //IEnumerable<ContactDto> productDto = await _iContactRepository.GetAllContacts();

            //return ;
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var response = new ResponseDto();

            try
            {
                // Fetch the contact by ID
                var contact = await _iContactRepository.GetContactById(id);

                if (contact != null)
                {
                    // Success case
                    response.IsSuccess = true;
                    response.Result = contact;
                    response.Message = "Contact retrieved successfully.";
                }
                else
                {
                    // Case when the contact is not found
                    response.IsSuccess = false;
                    response.Errors = new List<string> { $"Contact with ID {id} not found." };
                    response.Message = "No contact found.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                response.IsSuccess = false;
                response.Errors = new List<string> { ex.Message };
                response.Message = "An error occurred while retrieving the contact.";
            }

            // Return the response wrapped in an IActionResult
            return Ok(response);
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDto contactDto)
        {
            var response = new ResponseDto();

            try
            {
                // Input validation
                if (contactDto == null)
                {
                    response.IsSuccess = false;
                    response.Errors = new List<string> { "Contact data is required." };
                    response.Message = "Invalid input.";
                    return BadRequest(response);
                }

                // Call repository or service method to create/update the contact
                var result = await _iContactRepository.CreateUpdateContactAsync(contactDto);

                if (result != null)
                {
                    // Success case
                    response.IsSuccess = true;
                    response.Result = result;
                    response.Message = contactDto.Id == 0
                        ? "Contact created successfully."
                        : "Contact updated successfully.";
                }
                else
                {
                    // Case where the operation fails for some reason
                    response.IsSuccess = false;
                    response.Errors = new List<string> { "Failed to create or update contact." };
                    response.Message = "Operation failed.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                response.IsSuccess = false;
                response.Errors = new List<string> { ex.Message };
                response.Message = "An error occurred during the operation.";
            }

            // Return the response wrapped in an IActionResult
            return Ok(response);
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public void Put(int id,ContactDto contactDto)
        {
            //if (contactDto.Id == 0)
            //{
            //    // Create logic
            //    var newContact = new Contact
            //    {
            //        Name = contactDto.Name,
            //        Email = contactDto.Email,
            //        PhoneNumber = contactDto.PhoneNumber
            //    };

            //    _dbContext.Contacts.Add(newContact);
            //    await _dbContext.SaveChangesAsync();

            //    return new ContactDto
            //    {
            //        Id = newContact.Id,
            //        Name = newContact.Name,
            //        Email = newContact.Email,
            //        PhoneNumber = newContact.PhoneNumber
            //    };
            //}
            //else
            //{
            //    // Update logic
            //    var existingContact = await _dbContext.Contacts.FindAsync(contactDto.Id);
            //    if (existingContact != null)
            //    {
            //        existingContact.Name = contactDto.Name;
            //        existingContact.Email = contactDto.Email;
            //        existingContact.Phone = contactDto.Phone;

            //        _dbContext.Contacts.Update(existingContact);
            //        await _dbContext.SaveChangesAsync();

            //        return new ContactDto
            //        {
            //            Id = existingContact.Id,
            //            Name = existingContact.Name,
            //            Email = existingContact.Email,
            //            PhoneNumber = existingContact.PhoneNumber
            //        };
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new ResponseDto();

            try
            {
                // Attempt to delete the contact
                var isDeleted = await _iContactRepository.DeleteContactAsync(id);

                if (isDeleted)
                {
                    // Success case
                    response.IsSuccess = true;
                    response.Result = null;
                    response.Message = $"Contact with ID {id} deleted successfully.";
                }
                else
                {
                    // Case where the contact was not found or couldn't be deleted
                    response.IsSuccess = false;
                    response.Errors = new List<string> { $"Contact with ID {id} not found or could not be deleted." };
                    response.Message = "Failed to delete the contact.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                response.IsSuccess = false;
                response.Errors = new List<string> { ex.Message };
                response.Message = "An error occurred while deleting the contact.";
            }

            // Return the response wrapped in an IActionResult
            return Ok(response);
        }
    }
}
