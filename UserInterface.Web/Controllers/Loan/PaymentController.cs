using AutoMapper;
using Core.DataAccess.IRepository.Loan;
using Core.Domain.Loan;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Loan;

namespace UserInterface.Web.Controllers.Loan
{
    /// <summary>
    /// Payment Api Controller
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/loan/payment")]
    public class PaymentController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ApplicationDbContext _context;

        public PaymentController(
            IMapper mapper,
            IPaymentRepository paymentRepository,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _context = context;
        }

        /// <summary>
        /// Saves a Payment
        /// </summary>
        /// <param name="paymentModel">Payment data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] PaymentModel paymentModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var person = await _context.Persons.FindAsync(paymentModel.PersonId);

            if (person == null)
                return BadRequest("PersonId doesn't exists");

            var payment = _mapper.Map<Payment>(paymentModel);

            await _paymentRepository.SaveAsync(payment, cancellationToken);

            return Ok(paymentModel);
        }

        /// <summary>
        /// Updates a Payment
        /// </summary>
        /// <param name="paymentModel">Payment data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] PaymentModel paymentModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var payment = await _paymentRepository.GetByIdAsync(paymentModel.Id, cancellationToken);

                if (payment == null)
                    return NotFound(paymentModel.Id);

                payment = _mapper.Map(paymentModel, payment);
                await _paymentRepository.UpdateAsync(payment, cancellationToken);

                return Ok(paymentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a payment.
        /// </summary>
        /// 
        /// <param name="id">Payment Id to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var payment = await _paymentRepository.GetByIdAsync(id, cancellationToken);

            if (payment == null)
                return NotFound(id);

            try
            {
                await _paymentRepository.DeleteAsync(payment, cancellationToken);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all Payments
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _paymentRepository.GetAllAsync(cancellationToken);
            var payments = _mapper.Map<ICollection<PaymentModel>>(result);

            return Ok(payments);
        }

        /// <summary>
        /// Gets an Payment by Id.
        /// </summary>
        /// 
        /// <param name="id">Payment Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var payment = await _paymentRepository.GetByIdAsync(id, cancellationToken);

            if (payment == null)
                return NotFound(id);

            var paymentModel = _mapper.Map<PaymentModel>(payment);

            return Ok(paymentModel);
        }
    }
}
