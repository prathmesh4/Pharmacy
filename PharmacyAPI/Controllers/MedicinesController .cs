using Microsoft.AspNetCore.Mvc;
using PharmacyAPI.Models;

namespace PharmacyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {

        private readonly ILogger<MedicinesController> _logger;

        public MedicinesController(ILogger<MedicinesController> logger)
        {
            _logger = logger;
        }

        private static List<Medicine> _medicines = new List<Medicine>
        {
            new Medicine { Id = 1, FullName = "Medicine 1", Notes = "Some notes for medicine 1", ExpiryDate = DateTime.Now.AddDays(20), Quantity = 15, Price = 9.99m, Brand = "Brand X" },
            new Medicine { Id = 2, FullName = "Medicine 2", Notes = "Some notes for medicine 2", ExpiryDate = DateTime.Now.AddDays(50), Quantity = 5, Price = 14.99m, Brand = "Brand Y" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Medicine>> GetMedicines()
        {
            return _medicines;
        }

        [HttpGet("{id}")]
        public ActionResult<Medicine> GetMedicine(int id)
        {
            var medicine = _medicines.FirstOrDefault(m => m.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }
            return medicine;
        }

        [HttpPost]
        public ActionResult<Medicine> AddMedicine(Medicine medicine)
        {
            medicine.Id = _medicines.Count + 1;
            _medicines.Add(medicine);
            return CreatedAtAction(nameof(GetMedicine), new { id = medicine.Id }, medicine);
        }
    }
}
