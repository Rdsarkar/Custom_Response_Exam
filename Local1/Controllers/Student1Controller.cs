using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Local1.Models;
using Local1.DTOs;

namespace Local1
{
    public class SelfClass
    {
        public decimal? Id { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class Student1Controller : ControllerBase
    {
        private readonly ModelContext _context;

        public Student1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Student1
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetStudent1s()
        {
            List<Student1> student1s = await _context.Student1s
                                                     .OrderBy(i => i.Id)
                                                     .ToListAsync();

            if (student1s.Count <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "Data pacche nah",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Data pacche ",
                Success = true,
                Payload = student1s
            });
        }

        // GET: api/Student1/5
        [HttpPost("GetByID")]
        public async Task<ActionResult<ResponseDto>> GetStudent1([FromBody] SelfClass input)
        {
            if (input.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up the ID field",
                    Success = false,
                    Payload = null
                });
            }

            var student1 = await _context.Student1s.Where(i => i.Id == input.Id).FirstOrDefaultAsync();

            if (student1 == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "ID not found",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "ID found",
                Success = true,
                Payload = null
            });
        }

        // GET: api/Student1/Custom
        [HttpGet("GetCustom")]
        public async Task<ActionResult<ResponseDto>> GetStudent1c()
        {
            List<Student1> student1s = await _context.Student1s
                                                     .Where(i => i.Age > (decimal)9.5)
                                                     .OrderBy(i => i.Name)
                                                     .ToListAsync();

            if (student1s.Count <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "Data pacche nah",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Data pacche ",
                Success = true,
                Payload = student1s
            });
        }

        // PUT: api/Student1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("UpdateData")]
        public async Task<ActionResult<ResponseDto>> PutStudent1([FromBody] Student1 input)
        {
            if (input.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill UP ID field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up the Name field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Age == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "AGE is null",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Age == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "AGE cant be Zero",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Age < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "AGE cant be negative",
                    Success = false,
                    Payload = null
                });
            }

            var students1 = await _context.Student1s.Where(i => i.Id == input.Id).FirstOrDefaultAsync();
            if (students1 == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "This ID is not found in the database",
                    Success = false,
                    Payload = null
                });
            }
            students1.Name = input.Name;
            students1.Age = input.Age;


            _context.Student1s.Update(students1);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Server error so cant Update data",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "DataUpdated",
                Success = true,
                Payload = null
            });
        }

        // POST: api/Student1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("InsertNewData")]
        public async Task<ActionResult<ResponseDto>> PostStudent1([FromBody] Student1 input)
        {

            if (input.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up the ID field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up the Name field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Age == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "AGE is null",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Age == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "AGE cant be Zero",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Age < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "AGE cant be negative",
                    Success = false,
                    Payload = null
                });
            }
            var student1s = await _context.Student1s.Where(i => i.Id == input.Id).FirstOrDefaultAsync();

            _context.Student1s.Add(input);

            if (student1s != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ResponseDto
                {
                    Message = "Already same ID in the dataBase",
                    Success = false,
                    Payload = null
                });
            }
            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Data cant inserted for the Internal Server Error",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Data Inserted",
                Success = true,
                Payload = null
            });

        }

        // DELETE: api/Student1/5
        [HttpPost("DeleteData")]
        public async Task<ActionResult<ResponseDto>> DeleteStudent1([FromBody] SelfClass input)
        {
            if (input.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up the ID field",
                    Success = false,
                    Payload = null
                });
            }

            var student1s = await _context.Student1s.Where(i => i.Id == input.Id).FirstOrDefaultAsync();
            if (student1s == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "ID dont match with the database",
                    Success = false,
                    Payload = null
                });
            }

            _context.Student1s.Remove(student1s);
            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Data Cant delete for the internal server error",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Delete Done",
                Success = true,
                Payload = null
            });
        }

        private bool Student1Exists(decimal? id)
        {
            return _context.Student1s.Any(e => e.Id == id);
        }
    }
}
