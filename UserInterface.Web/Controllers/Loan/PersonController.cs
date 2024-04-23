using AutoMapper;
using Core.DataAccess.IRepository.Loan;
using Core.Domain.Loan;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Loan;

namespace UserInterface.Web.Controllers.Loan
{
    /// <summary>
    /// Person Api Controller
    /// </summary>
    [ApiController]
    [Route("api/loan/person")]
    public class PersonController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly ApplicationDbContext _context;

        public PersonController(
            IMapper mapper,
            IPersonRepository personRepository,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _context = context;
        }

        /// <summary>
        /// Saves a Person
        /// </summary>
        /// <param name="personModel">Person data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] PersonModel personModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var personExists = _personRepository.FindPersonByName(personModel.Name);
            if (personExists != null)
                return StatusCode(StatusCodes.Status403Forbidden, "The Person already exists");

            var person = _mapper.Map<Person>(personModel);

            await _personRepository.SaveAsync(person, cancellationToken);

            return Ok(personModel);
        }

        /// <summary>
        /// Updates a Person
        /// </summary>
        /// <param name="personModel">Person data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] PersonModel personModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var person = await _personRepository.GetByIdAsync(personModel.Id, cancellationToken);

                if (person == null)
                    return NotFound(personModel.Id);

                person = _mapper.Map(personModel, person);
                await _personRepository.UpdateAsync(person, cancellationToken);

                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a person.
        /// </summary>
        /// 
        /// <param name="id">Person Id to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var person = await _personRepository.GetByIdAsync(id, cancellationToken);

            if (person == null)
                return NotFound(id);

            try
            {
                await _personRepository.DeleteAsync(person, cancellationToken);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all Persons
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _personRepository.GetAllAsync(cancellationToken);
            var persons = _mapper.Map<ICollection<PersonModel>>(result);

            return Ok(persons);
        }

        /// <summary>
        /// Gets an Person by Id.
        /// </summary>
        /// 
        /// <param name="id">Person Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var person = await _personRepository.GetByIdAsync(id, cancellationToken);

            if (person == null)
                return NotFound(id);

            var personModel = _mapper.Map<PersonModel>(person);

            return Ok(personModel);
        }

        #region Payment

        /// <summary>
        /// Gets all Payments Person by Id
        /// </summary>
        /// 
        /// <param name="id">Person Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}/payments")]
        public async Task<IActionResult> GetPaymentsByPersonId(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var person = await _personRepository.GetByIdAsync(id, cancellationToken);

            if (person == null)
                return NotFound(id);

            var result = _context.Payments.Where(p => p.PersonId == person.Id);
            var payments = _mapper.Map<ICollection<PaymentModel>>(result);

            return Ok(payments);
        }

        #endregion
    }
}
